using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI Name;
    [SerializeField] TextMeshProUGUI Description;


    public void SetUpgradeUI(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        Name.text = upgradeData.Name;
        Description.text = upgradeData.Description;
    }

    public void Clean()
    {
        icon.sprite = null;
        Name.text = null;
        Description.text = null;
    }
}
