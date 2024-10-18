using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItems : MonoBehaviour
{
    [SerializeField] List<ItemData> items;
    [SerializeField] ItemData healthIncreaseItem;

    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Start()
    {
        //Equip(healthIncreaseItem);
    }

    public void Equip(ItemData itemToEquip)
    {
        if (items == null)
        {
            items = new List<ItemData>();
        }
        items.Add(itemToEquip);
        itemToEquip.Equip(player);
    }

    public void UnEquip(ItemData itemToUnEquip)
    {
        items.Remove(itemToUnEquip);
    }
}
