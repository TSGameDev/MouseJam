using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : State
{
    private float _CurrentTime;

    public AttackPlayer(AIEntity AIEntity) : base(AIEntity) { }

    public override void Enter()
    {
        _Animator.SetBool(_AIEntity.ANIMHASH_ATTACK, true);
        _Animator.SetBool(_AIEntity.ANIMHASH_MOVING, false);
    }

    public override void Update()
    {
        CalculatePlayerTracking();
    }

    private void CalculatePlayerTracking()
    {
        float _DisToPlayer = Vector2.Distance(
            new Vector2(_RigidBody.position.x, _RigidBody.position.y),
            new Vector2(_Player.transform.position.x, _Player.transform.position.y));

        //Of distance is greater then attack range, begin tracking player/moving towards player
        if (_DisToPlayer > _AIEntity.GetInstanceStats().attackRange)
        {
            if (_AIEntity.GetIsFly())
                _StateMachine.CurrentState = new TrackPlayerFly(_AIEntity);
            else
                _StateMachine.CurrentState = new TrackPlayer(_AIEntity);
        }
    }

    public override void Exit()
    {
        _Animator.SetBool(_AIEntity.ANIMHASH_ATTACK, false);
    }
}
