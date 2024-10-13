using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeState : State
{

    [SerializeField] UpgradeManager upgradeManager;
    [SerializeField] EnemyManager myEnemyManager;

    public override void UpdateState()
    {
        base.UpdateState();
        myEnemyManager.StopAllEnemies();
    }

}
