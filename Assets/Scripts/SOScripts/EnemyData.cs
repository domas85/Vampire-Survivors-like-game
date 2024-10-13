using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyType
{
    CommonEnemy = 0, NormalEnemy, EliteEnemy, EliteEnemyOther
}
[CreateAssetMenu(fileName = "NewEnemy", menuName = "GamesObjectData/NewEnemyType", order = 1)]
public class EnemyData : ScriptableObject
{
    public EnemyType enemyType;
    public string Name;
    public int maxHealth;
    [HideInInspector] public int Health;
    public Sprite enemySprite;
    public GameObject expDrop;
    public float enemyMoveSpeed;
    public float enemyDamage;
    public float enemyDamageMultiplier = 1f;
    public float knockbackForce = 4f;
    public AnimationCurve enemySpawnRarityPerLevel;
}
