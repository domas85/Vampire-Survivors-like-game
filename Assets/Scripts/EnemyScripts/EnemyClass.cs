using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyClass : MonoBehaviour
{
    public EnemyData enemyData;

    public virtual void UpdateLogic(Transform aPlayerPos)
    {

    }

    public virtual void StopEnemy()
    {

    }
}
