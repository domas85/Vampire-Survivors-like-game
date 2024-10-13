using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public List<State> states = new List<State>();
    public State CurrentState = null;

    public void SwitchState<aState>()
    {
        foreach (State s in states)
        {
            if (s.GetType() == typeof(aState))
            {
                CurrentState?.ExitState();
                CurrentState = s;
                CurrentState.EnterState();
                break;
            }
        }
        //Debug.LogWarning("State Does not exits");
        Debug.LogWarning("Switched States");
    }

    public virtual void UpdateStateMachine()
    {
        CurrentState?.UpdateState();
    }

    public virtual void FixedUpdateStateMachine()
    {
        CurrentState?.FixedUpdateState();
    }

    public bool IsState<aState>()
    {
        if (!CurrentState) return false;
        return CurrentState.GetType() == typeof(aState);
    }


}
