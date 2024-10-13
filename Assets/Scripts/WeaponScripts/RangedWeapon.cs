using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : WeaponBase
{
    [SerializeField] float timeToAttack;
    float timer;


    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform BowSprite;


    [SerializeField] public float bulletSpeed = 1;
    [SerializeField] public float bulletSpeedMultiplier = 1;
    [SerializeField] public float bulletDuration = 3;

    public Camera cam;

    private Vector3 mousePosition;
    private Vector3 lookAtPosition;
    private Vector2 direction;


    [SerializeField] Transform weaponPivotPoint;

    [SerializeField] private bool rotateToMouse = false;

    private void Awake()
    {
        if (Camera.main != null)
        {
            cam = Camera.main;
            cam.enabled = true;
        }
    }

    public override void UpdateWeapon()
    {

        if (cam == null && rotateToMouse)
        {
            Debug.LogError(gameObject.name + " target missing!");
            return;
        }

        if (rotateToMouse == true)
        {
            mousePosition = Input.mousePosition;
            mousePosition.z = -cam.transform.position.z + weaponPivotPoint.transform.position.z;

            lookAtPosition = cam.ScreenToWorldPoint(mousePosition);

            direction = (lookAtPosition - weaponPivotPoint.transform.position).normalized;

            Debug.DrawRay(weaponPivotPoint.transform.position, direction * 20f, Color.blue);
        }
        weaponPivotPoint.up = direction;


        SpawnBulletsOverTime();
    }


    //Bullets dont stop when the game is paused
    private void SpawnBullets()
    {
        GameObject bullet = Instantiate(bulletPrefab, weaponPivotPoint.transform.position, Quaternion.identity);
        bullet.transform.up = direction;


        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed * bulletSpeedMultiplier; // having 2 get componets is not ideal, could not find a quick solution

        bullet.GetComponent<WeaponBase>().weaponStats = weaponStats;


        StartCoroutine(DestroyWithDelay(bulletDuration, bullet));
    }
    private void SpawnBulletsOverTime()
    {
        if (timer < timeToAttack)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0;
        SpawnBullets();
    }

    IEnumerator DestroyWithDelay(float delay, GameObject bullet)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }

}
