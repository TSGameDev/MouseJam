using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : State
{
    private IDamagable _PlayerDamage;

    private float _CurrentTime;

    public AttackPlayer(AIEntity AIEntity) : base(AIEntity) { }

    public override void Enter()
    {
        _PlayerDamage = _Player.GetComponent<IDamagable>();
    }

    public override void Update()
    {
        CalculatePlayerTracking();
        AttackTimer();
    }

    private void CalculatePlayerTracking()
    {
        float _DisToPlayer = Vector2.Distance(
            new Vector2(_GameObject.transform.position.x, _GameObject.transform.position.y),
            new Vector2(_Player.transform.position.x, _Player.transform.position.y));

        //Of distance is greater then attack range, begin tracking player/moving towards player
        if (_DisToPlayer >= _AIEntity.GetInstanceStats().attackRange)
        {
            if (_AIEntity.GetIsFly())
                _StateMachine.CurrentState = new TrackPlayerFly(_AIEntity);
            else
                _StateMachine.CurrentState = new TrackPlayer(_AIEntity);
        }
    }

    private void AttackTimer()
    {
        if (_CurrentTime >= _AIEntity.GetInstanceStats().timeBetterAttacks)
        {
            PerformAttack();
            _CurrentTime = 0;
        }
        else
            _CurrentTime += 1 * Time.deltaTime;
    }

    private void PerformAttack()
    {
        _PlayerDamage?.Damage(_AIEntity.GetInstanceStats().attackDamage);

        if (_AIEntity.GetIsSuicide())
            _AIEntity.Death();
        else
            _Animator.SetTrigger(_AIEntity.ANIMHASH_ATTACK);
    }
}
