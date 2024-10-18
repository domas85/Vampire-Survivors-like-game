using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    WeaponUnlock = 0, WeaponUpgrade, ItemUnlock, ItemUpgrade
}
[CreateAssetMenu(fileName = "NewUpgrade", menuName = "GamesObjectData/NewUpgradeType", order = 1)]
public class UpgradeData : ScriptableObject
{

    public UpgradeType upgradeType;
    public string Name;
    public string Description;
    public Sprite icon;

    public WeaponData weaponData;
    public WeaponStats weaponUpgradeStats;
}
