using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UpgradeManager : MonoBehaviour
{
    [SerializeField] EnemyManager myEnemyManager;


    public PlayerData playerData;
    int Exp = 0;
    public int CoinExpReward = 50;
    [SerializeField] ExpUI experienceBar;

    [SerializeField] List<UpgradeButton> ButtonList;
    [SerializeField] List<UpgradeData> upgrades;
    List<UpgradeData> selectedUpgrade;

    [SerializeField] public List<UpgradeData> acquiredUpgrades;

    bool noMoreUpgrades = false;
    [SerializeField] GameObject upgradeMenu;
    WeaponManager weaponManager;
    [SerializeField] Animator UIAnanimator; 


    private void Awake()
    {
        playerData.Level = 1;
        weaponManager = GetComponent<WeaponManager>();

    }

    int EXP_TO_LEVEL_UP
    {
        get
        {
            return playerData.Level * 300;
        }
    }

    private void Start()
    {
        HideButtons();
        experienceBar.UpdateExpBar(Exp, EXP_TO_LEVEL_UP);
        experienceBar.SetLevelText(playerData.Level);
        int indexToRemove = -1;
        for (int i = 0; i < upgrades.Count; i++)
        {
            if (upgrades[i].weaponData == weaponManager.startingWeapon)
            {
                indexToRemove = i;
                break;
            }
        }
        if (indexToRemove != -1)
        {
            //acquiredUpgrades.Add(upgrades[indexToRemove]);
            upgrades.RemoveAt(indexToRemove);
        }

    }

    public void AddExperience(int amount)
    {
        Exp += amount;
        LevelUpCheck();
        experienceBar.UpdateExpBar(Exp, EXP_TO_LEVEL_UP);

    }

    public void Upgrade(int pressedButton)
    {
        UpgradeData upgradeData = selectedUpgrade[pressedButton];

        if (acquiredUpgrades == null) { acquiredUpgrades = new List<UpgradeData>(); }

        switch (upgradeData.upgradeType)
        {
            case UpgradeType.WeaponUnlock:
                AddAvailableUpgrades(upgradeData.weaponData.upgrades);
                OrganizeUpgradeData(upgradeData);
                weaponManager.AddWeapon(upgradeData.weaponData);
                break;
            case UpgradeType.WeaponUpgrade:
                weaponManager.UpgradeWeapon(upgradeData);
                OrganizeUpgradeData(upgradeData);
                break;
            case UpgradeType.ItemUnlock:

                OrganizeUpgradeData(upgradeData);
                break;
            case UpgradeType.ItemUpgrade:

                OrganizeUpgradeData(upgradeData);
                break;
        }
    }

    public void OrganizeUpgradeData(UpgradeData upgradeData)
    {
        acquiredUpgrades.Add(upgradeData);
        upgrades.Remove(upgradeData);
        HideButtons();
        upgradeMenu.SetActive(false);
        if (upgrades.Count == 0)
        {
            noMoreUpgrades = true;
        }

        GamesManager.Instance.SwitchState<PlayingState>();
    }

    public void LevelUpCheck()
    {
        if (Exp >= EXP_TO_LEVEL_UP)
        {
            Exp -= EXP_TO_LEVEL_UP;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if (selectedUpgrade == null) { selectedUpgrade = new List<UpgradeData>(); }
        selectedUpgrade.Clear();
        selectedUpgrade.AddRange(GetUpgrades(3));
        
        playerData.Level++;

        if(myEnemyManager.spawnRate > 0.1f && playerData.Level % 2 != 0 )
        {
            IncreaseDifficulty();
        }
        //foreach(EnemyClass aEnemy in myEnemyManager.enemiesList)
        //{
        //    aEnemy.enemyData.enemySpawnRarityPerLevel.Evaluate(playerData.Level);
        //}

        //myEnemyManager.enemiesList[1].enemyWeight = enemyWeightIncrease.Evaluate(playerData.Level);

        experienceBar.SetLevelText(playerData.Level);
        Debug.Log("the level is: " + playerData.Level);
        UIAnanimator.SetTrigger("NewLevel");
        if (noMoreUpgrades == false)
        {
            GamesManager.Instance.SwitchState<UpgradeState>();
        }
        else
        {
            GamesManager.Instance.SwitchState<PlayingState>();
        }
        OpenUpgradeMenu(selectedUpgrade);
    }

    public void IncreaseDifficulty()
    {
        myEnemyManager.spawnRate -= 0.05f;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickupEXP"))
        {
            AddExperience(CoinExpReward);
            Destroy(collision.gameObject);
            Debug.Log("the exp is: " + Exp);
        }
    }

    public void OpenUpgradeMenu(List<UpgradeData> upgradeData)
    {
        Clean();
        experienceBar.UpdateExpBar(Exp, EXP_TO_LEVEL_UP);
        upgradeMenu.SetActive(true);
        Debug.Log("there are:" + upgrades.Count + " upgrades to get");

        for (int i = 0; i < upgradeData.Count; i++)
        {
            ButtonList[i].gameObject.SetActive(true);
            ButtonList[i].SetUpgradeUI(upgradeData[i]);
        }
    }

    public void Clean()
    {
        for (int i = 0; i < ButtonList.Count; i++)
        {
            ButtonList[i].Clean();
        }
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgrades.Count)
        {
            count = upgrades.Count;
        }

        for (int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
    }


    private void HideButtons()
    {
        for (int i = 0; i < ButtonList.Count; i++)
        {
            ButtonList[i].gameObject.SetActive(false);
        }
    }

    public void AddAvailableUpgrades(List<UpgradeData> upgradesToAdd)
    {
        upgrades.AddRange(upgradesToAdd);
    }

}
