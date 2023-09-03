using Pathfinding;
using System;
using UnityEngine;

public abstract class State
{
    protected AIEntity _AIEntity;
    protected GameObject _GameObject;
    protected StateMachine _StateMachine;
    protected Animator _Animator;
    protected Rigidbody2D _RigidBody;
    protected Seeker _Seeker;
    protected GameObject _Player;

    public State(AIEntity _AIEntity)
    {
        this._AIEntity = _AIEntity;
        _GameObject = _AIEntity.gameObject;
        _StateMachine = _AIEntity.GetStateMachine();
        _Animator = _AIEntity.GetAnimator();
        _RigidBody = _AIEntity.GetRigidbody2D();
        _Seeker = _AIEntity.GetSeeker();
        _Player = _AIEntity.GetPlayer();
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void Exit() { }
}
