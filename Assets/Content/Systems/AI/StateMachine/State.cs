using System;
using UnityEngine;

public abstract class State
{
    protected GameObject _GameObject;
    protected StateMachine _StateMachine;
    protected Animator _Animator;
    protected Rigidbody2D _RigidBody;
    protected AIEntity _AIEntity;

    public State(GameObject GameObject, StateMachine StateMachine, Animator Animator, Rigidbody2D RigidBody, AIEntity AIEntity)
    {
        _GameObject = GameObject;
        _StateMachine = StateMachine;
        _Animator = Animator;
        _AIEntity = AIEntity;
        _RigidBody = RigidBody;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}
