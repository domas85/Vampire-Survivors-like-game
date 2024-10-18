using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : State
{
    //[SerializeField] EnemyManager myEnemyManager;
    [SerializeField] GameObject pauseMenu;




    public override void UpdateState()
    {
        base.UpdateState();

        //myEnemyManager.StopAllEnemies();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.gameObject.SetActive(false);
            GamesManager.Instance.SwitchState<PlayingState>();
        }

    }
}
