using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "NewItem", menuName = "GamesObjectData/NewItem", order = 1)]
public class ItemData : ScriptableObject
{
    public string Name;
    public int armour;
    public int increaseMaxHp;

    public void Equip(Player player)
    {
        player.playerData.maxHealth += increaseMaxHp;
    }
    public void UnEquip(Player player)
    {
        player.playerData.maxHealth -= increaseMaxHp;
    }
}
