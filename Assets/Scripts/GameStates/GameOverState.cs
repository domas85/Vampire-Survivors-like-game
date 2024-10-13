using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : State
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] EnemyManager myEnemyManager;

    public override void UpdateState()
    {
        base.UpdateState();

        myEnemyManager.StopAllEnemies();
        gameOverScreen.SetActive(true);

    }
}
