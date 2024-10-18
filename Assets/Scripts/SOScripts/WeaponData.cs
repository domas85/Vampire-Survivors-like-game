using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public int damage;
    public int damageMultiplier;


    public WeaponStats(int damage, int damageMultiplier)
    {
        this.damage = damage;
        this.damageMultiplier = damageMultiplier;
    }

    public void Sum(WeaponStats weaponUpgradeStats)
    {
        damage += weaponUpgradeStats.damage;
        damageMultiplier += weaponUpgradeStats.damageMultiplier;
    }
}

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "GamesObjectData/NewWeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public string Name;
    public string Description;
    public WeaponStats stats;
    public GameObject weaponBasePrefab;
    public List<UpgradeData> upgrades;
}
