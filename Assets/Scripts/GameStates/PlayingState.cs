using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayingState : State
{
    [SerializeField] Player myPlayer;
    [SerializeField] EnemyManager myEnemyManager;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] BackgroundScroll background;



    public override void UpdateState()
    {
        base.UpdateState();
      
        myPlayer.PlayerUpdate();
        myPlayer.UpdateGameTimer();
        myEnemyManager.EnemySpawnerUpdate();
        //myEnemyManager.EnemyLogicUpdate();
        background.UpdateBackground();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.gameObject.SetActive(true);
            myEnemyManager.StopAllEnemies();
            GamesManager.Instance.SwitchState<PauseState>();
        }

    }

    public override void FixedUpdateState()
    {
        base.UpdateState();

        myEnemyManager.EnemyLogicUpdate();

    }
}
