using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum WEAPON_TYPE { melee = 0, ranged, bullet, orbiter, passiveWeapon };

public class WeaponBase : MonoBehaviour
{
    public WEAPON_TYPE weaponType;

    public WeaponData weaponData;

    public WeaponStats weaponStats;

    //[SerializeField] public float bulletDuration = 3;

    [SerializeField] public float passiveDamageIntervals = 1;


    public void Update()
    {
        if (GamesManager.Instance.CurrentState == GamesManager.Instance.states[0])
        {
            UpdateWeapon();
        }
    }

    public virtual void UpdateWeapon()
    {

    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        weaponStats = new WeaponStats(wd.stats.damage, wd.stats.damageMultiplier);
    }

    public void Upgrade(UpgradeData upgradeData)
    {
        weaponStats.Sum(upgradeData.weaponUpgradeStats);
    }

}
