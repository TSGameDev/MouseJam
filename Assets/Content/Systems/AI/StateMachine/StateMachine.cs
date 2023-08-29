using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private State _CurrentState;
    public State CurrentState
    {
        get => _CurrentState;
        set
        {
            if(_CurrentState != null)
                _CurrentState.Exit();

            _CurrentState = value;
            
            if (_CurrentState != null)
                _CurrentState.Enter();
        }
    }

    public void UpdateCurrentState() => _CurrentState.Update();
    public void FixedUpdateCurrentState() => _CurrentState.FixedUpdate();
}
