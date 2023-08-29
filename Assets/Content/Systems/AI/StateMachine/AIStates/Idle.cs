using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    public Idle(GameObject GameObject, StateMachine StateMachine, Animator Animator, Rigidbody2D RigidBody, AIEntity AIEntity)
        : base(GameObject, StateMachine, Animator, RigidBody, AIEntity) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void Exit() 
    {
        base.Exit();
    }
}
