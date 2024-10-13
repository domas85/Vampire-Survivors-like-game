using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{

    //static GameObject VFX_EnemyDeath;

    //public void Start()
    //{
    //    VFX_EnemyDeath = Resources.Load<GameObject>("DeathFX");
    //}
    public static void SpawnDeathFX(Vector3 aPosition, GameObject vfx)
    {
        Instantiate(vfx, aPosition, Quaternion.identity);
    }
}
   
