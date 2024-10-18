using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesManager : StateMachine
{
    #region Singleton
    public static GamesManager Instance;

    private void Awake()
    {
        Instance = this;
    }
 
    #endregion


    private void Start()
    {
        SwitchState<PlayingState>();
    }

    public void FixedUpdate()
    {
        FixedUpdateStateMachine();
    }

    private void Update()
    {
        UpdateStateMachine();


    }

}
