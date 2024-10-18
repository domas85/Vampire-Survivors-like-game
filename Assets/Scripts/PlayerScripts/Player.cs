using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : Damageable
{
    public PlayerData playerData;

    [SerializeField] TextMeshProUGUI gameTimer;
    private float internalTimer;

    [SerializeField] HealthUI healthBar;
    [SerializeField] ExpUI experienceBar;

    [SerializeField] CircleCollider2D PlayerColllider;
    [SerializeField] SpriteRenderer playerSprite;
    float moveH, moveV;
    float timePlaying = 0;

    void Start()
    {
        playerData.Health = playerData.maxHealth;
    }

    public void PlayerUpdate()
    {
        healthBar.SetHealth(playerData.Health);

        Movement();
    }


    private void Movement()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");

        Vector3 velocity = new Vector3(moveH, moveV, 0).normalized;
        transform.position += velocity * playerData.moveSpeed * Time.deltaTime;

    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        //if (collision.tag == "Enemy")
        //{
        //    Enemy enemyAttack = collision.GetComponent<Enemy>();
        //    var damageTaken = enemyAttack.enemyDamage * enemyAttack.enemyDamageMultiplier;
        //    TakeDamage(damageTaken);
        //}


        EnemyClass enemy = collision.GetComponent<EnemyClass>();

        if (enemy != null)
        {
            TakeDamage(enemy.enemyData.enemyDamage * enemy.enemyData.enemyDamageMultiplier);
            //Debug.Log("Player took " + (enemy.enemyDamage * enemy.enemyDamageMultiplier) + " damage");
            //EnemySpawner.enemyPool.Release(enemy);

            //Destroy(enemy.gameObject);

        }


    }

    public void UpdateGameTimer()
    {

        timePlaying += Time.deltaTime;
        playerData.timePlaying = timePlaying;

        var myTimeSpan = TimeSpan.FromSeconds(timePlaying);


        var output = $"{(int)myTimeSpan.TotalMinutes}:{myTimeSpan.Seconds:00}";

        String timeStr = output;
        gameTimer.text = timeStr;


    }

    public void TakeDamage(float someDamage)
    {
        playerData.Health -= someDamage;
        StartCoroutine(playerColorFlash(playerSprite));
        if (playerData.Health < 0) Death();
        Debug.Log("Player took " + (someDamage) + " damage");

    }

    void Death()
    {
        GamesManager.Instance.SwitchState<GameOverState>();
        Debug.Log("Player died");
    }


    IEnumerator playerColorFlash(SpriteRenderer originalSprite)
    {
        originalSprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        originalSprite.color = Color.white;
    }
}
