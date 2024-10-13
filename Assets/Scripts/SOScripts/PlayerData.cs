using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerData", menuName = "GamesObjectData/NewPlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField] public float maxHealth;
    public float Health;
    [SerializeField] public float moveSpeed;
    public string playerName;
    public float timePlaying;
    public int Level;

}
