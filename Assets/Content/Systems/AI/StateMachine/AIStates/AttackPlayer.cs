using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : State
{
    private GameObject _Player;
    private IDamagable _PlayerDamage;

    private float _CurrentTime;

    public AttackPlayer(GameObject GameObject, StateMachine StateMachine, Animator Animator, Rigidbody2D RigidBody, AIEntity AIEntity, GameObject Player)
        : base(GameObject, StateMachine, Animator, RigidBody, AIEntity)
    {
        _Player = Player;
    }

    public override void Enter()
    {
        Debug.Log("Enter Attacking State");
        _PlayerDamage = _Player.GetComponent<IDamagable>();
        PerformAttack();
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
            _StateMachine.CurrentState = new TrackPlayer(_GameObject, _StateMachine, _Animator, _RigidBody, _AIEntity, _Player);
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
        Debug.Log("Attacking");
        _PlayerDamage?.Damage(_AIEntity.GetInstanceStats().attackDamage);
    }
}
