using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : EnemyClass
{
    [SerializeField] SpriteRenderer enemySpriteRenderer;
    private float knockbackTimer;
    private float nextPassiveAttack = 0;

    private Player playerTransform;
    private Vector3 moveDirection;
    [SerializeField] private Rigidbody2D rb2D;

    private EnemyMovement enemyMovement;
    static GameObject VFX_EnemyDeath;

    void Start()
    {

        enemyMovement = GetComponent<EnemyMovement>();

        enemyMovement.myEnemyData = enemyData;

        enemySpriteRenderer.sprite = enemyData.enemySprite;
        VFX_EnemyDeath = Resources.Load<GameObject>("DeathFX");
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (playerTransform == null)
        {
            Debug.Log("Can't find player!!");
        }

        enemyData.Health = enemyData.maxHealth;
    }

    public override void UpdateLogic(Transform aPlayerPos)
    {
        nextPassiveAttack += Time.deltaTime;
        if (!enemyMovement) return;
        if (Time.time > knockbackTimer)
        {
            rb2D.velocity = enemyMovement.Move(aPlayerPos.position, rb2D);
        }
    }

    public override void StopEnemy()
    {
        rb2D.velocity = new Vector2(0, 0);
        rb2D.angularVelocity = Mathf.Atan2(0, 0) * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        WeaponBase weapon = collision.GetComponent<WeaponBase>();

        if (weapon != null)
        {
            if (weapon.weaponType == WEAPON_TYPE.passiveWeapon)
            {
                //TakeDamage(weapon.weaponStats.damage * weapon.weaponStats.damageMultiplier);
            }
            else
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;

                Vector2 knockback = -direction * enemyData.knockbackForce;
                rb2D.AddForce(knockback, ForceMode2D.Impulse);
                knockbackTimer = Time.time + 0.1f;

                TakeDamage(weapon.weaponStats.damage * weapon.weaponStats.damageMultiplier);
            }

            if (weapon.weaponType == WEAPON_TYPE.bullet)
            {
                Destroy(weapon.gameObject);
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        WeaponBase weapon = collision.GetComponent<WeaponBase>();

        if (weapon != null && weapon.weaponType == WEAPON_TYPE.passiveWeapon)
        {

            if (nextPassiveAttack >= weapon.passiveDamageIntervals)
            {
                TakeDamage(weapon.weaponStats.damage * weapon.weaponStats.damageMultiplier);
                nextPassiveAttack = 0;
            }
        }
    }

    public void TakeDamage(int someDamage)
    {
        enemyData.Health -= someDamage;
        Debug.Log("Enemy took " + someDamage + " damage");
        if (enemyData.Health <= 0) Death();
    }

    public void EnemyKnockback(Transform aPlayerPos)
    {
        Vector3 Direction = (aPlayerPos.position - transform.position).normalized;
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        rb2D.rotation = angle;
        moveDirection = Direction;

        rb2D.velocity = new Vector2(moveDirection.x - enemyData.knockbackForce, moveDirection.y - enemyData.knockbackForce);
    }



    void Death()
    {
        Effects.SpawnDeathFX(transform.position, VFX_EnemyDeath);
        Instantiate(enemyData.expDrop, transform.position, Quaternion.identity);
        EnemyManager.enemyPool.Release(this);
        enemyData.Health = enemyData.maxHealth;
        //Debug.Log("Enemy died");
    }
}



