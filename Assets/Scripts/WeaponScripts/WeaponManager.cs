using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponContainer;
    [SerializeField] public WeaponData startingWeapon;
    [SerializeField] UpgradeManager upgradesManager;
    List<WeaponBase> weapons;

    private void Awake()
    {
        weapons = new List<WeaponBase>();
    }

    private void Start()
    {
        upgradesManager.AddAvailableUpgrades(startingWeapon.upgrades);
        AddWeapon(startingWeapon);
        
    }

    public void AddWeapon(WeaponData weaponData)
    {
 
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponContainer);
        WeaponBase weaponBase = weaponGameObject.GetComponentInChildren<WeaponBase>();
        weaponBase.SetData(weaponData);
        weapons.Add(weaponBase);

        //if (upgradesManager != null)
        //{
        //    upgradesManager.AddAvailableUpgrades(weaponData.upgrades);
        //}

    }
    public void UpgradeWeapon(UpgradeData upgradeData)
    {
        WeaponBase weaponToUpgrade = weapons.Find(wd => wd.weaponData == upgradeData.weaponData);

        weaponToUpgrade.Upgrade(upgradeData);
    }

}
