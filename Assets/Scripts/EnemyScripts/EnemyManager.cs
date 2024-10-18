using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.EventSystems.EventTrigger;


public class EnemyManager : MonoBehaviour
{
    public Player player;

    public int spawnCount = 1;
    public float spawnRate = 1f;

    private float timeSinceLastSpawn;

    public static ObjectPooler<EnemyClass> enemyPool;
    public List<EnemyClass> enemyPooledList = new();


    public List<EnemyClass> enemiesList;
    private readonly string enemiePrefabPath = "Assets/Prefabs/Enemies/EnemyPrefabs";
    List<GameObject> allPrefabs;

    public Transform bottomSpawn;
    public Transform topSpawn;

    private void GetAllPrefabs()
    {
        string[] foldersToSearch = { enemiePrefabPath };
        enemiesList = GetAssets<EnemyClass>(foldersToSearch, "t:prefab");
    }

    public static List<T> GetAssets<T>(string[] _foldersToSearch, string _filter) where T : UnityEngine.Object
    {
        string[] guids = AssetDatabase.FindAssets(_filter, _foldersToSearch);
        List<T> a = new List<T>();
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            T g = AssetDatabase.LoadAssetAtPath<T>(path);
            a.Add(g);
        }
        return a;
    }

    private void Awake()
    {
        GetAllPrefabs();
        // if have time make more pools for diffrent objects i.e. for the exp coins and maybe projectiles
        enemyPool = new ObjectPooler<EnemyClass>(CreateEnemy, OnEnemyGet, OnEnemyRelease, true, 10, 1000);
    }

    #region Pool Fuctions and Actions
    private EnemyClass CreateEnemy(EnemyClass enemyInstance)
    {
        EnemyClass enemy;

        enemy = Instantiate(enemyInstance, SpawnPoint(), Quaternion.identity, this.transform);
        enemyPooledList.Add(enemy);
        enemy.gameObject.SetActive(true);
        return enemy;
    }

    private void OnEnemyGet(EnemyClass enemy)
    {
        enemy.gameObject.transform.position = SpawnPoint();
        enemy.gameObject.SetActive(true);
        if (enemyPooledList.Contains(enemy) == false)
        {
            enemyPooledList.Add(enemy);
        }
    }

    private void OnEnemyRelease(EnemyClass enemy)
    {
        foreach (EnemyClass thisEnemy in enemyPooledList)
        {
            thisEnemy.StopEnemy();
        }
        enemy.gameObject.SetActive(false);
    }
    #endregion


    public void StopAllEnemies()
    {
        foreach (EnemyClass thisEnemy in enemyPooledList) //(KeyValuePair<int, EnemyClass> thisEnemy in enemyDictionary)
        {
            thisEnemy.StopEnemy();
        }
    }

    public void EnemySpawnerUpdate()
    {
        if (Time.time > timeSinceLastSpawn)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                EnemyClass e = GetWeightedEnemyRandom();

                enemyPool.Get(e);

            }
            timeSinceLastSpawn = Time.time + spawnRate;
        }
    }

    public void EnemyLogicUpdate()
    {
        foreach (EnemyClass thisEnemy in enemyPooledList) //(KeyValuePair<int, EnemyClass> thisEnemy in enemyDictionary)
        {
            thisEnemy.UpdateLogic(player.transform);
        }
    }

    public EnemyClass GetWeightedEnemyRandom()
    {
        float totalChance = 0f;

        foreach (EnemyClass thisEnemy in enemiesList)
        {
            totalChance += thisEnemy.enemyData.enemySpawnRarityPerLevel.Evaluate(player.playerData.Level);
        }

        float rand = UnityEngine.Random.Range(0f, totalChance);
        float cumulativeChance = 0f;

        foreach (EnemyClass thisEnemy in enemiesList)
        {
            cumulativeChance += thisEnemy.enemyData.enemySpawnRarityPerLevel.Evaluate(player.playerData.Level);
            if (rand <= cumulativeChance)
            {
                return thisEnemy;
            }
        }
        return null;
    }

    public Vector3 SpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVertical = UnityEngine.Random.Range(0f, 1f) > .5f;

        if (spawnVertical)
        {
            spawnPoint.y = UnityEngine.Random.Range(bottomSpawn.position.y, topSpawn.position.y);

            if (UnityEngine.Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = topSpawn.position.x;
            }
            else
            {
                spawnPoint.x = bottomSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = UnityEngine.Random.Range(bottomSpawn.position.x, topSpawn.position.x);

            if (UnityEngine.Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = topSpawn.position.y;
            }
            else
            {
                spawnPoint.y = bottomSpawn.position.y;
            }
        }
        return spawnPoint;
    }

}
