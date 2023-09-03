using UnityEngine;

public class TrackPlayer : State
{
    private Vector2 _DesiredVelocity;
    private Vector2 _Velocity;
    private float _MaxSpeedChange;

    public TrackPlayer(AIEntity AIEntity) : base(AIEntity) { }

    public override void Enter()
    {
        _Animator.SetBool(_AIEntity.ANIMHASH_ATTACK, false);
        _Animator.SetBool(_AIEntity.ANIMHASH_MOVING, true);
    }

    public override void Update()
    {
        CalculatePlayerTracking();

        Vector3 _DirToPlayer = (_Player.transform.position - _GameObject.transform.position).normalized;

        if (_DirToPlayer.x > 0)
        {
            if(_AIEntity.GetInverseVisuals())
                _GameObject.transform.localScale = new Vector3(-1, 1, 1);
            else
                _GameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_DirToPlayer.x < 0)
        {
            if (_AIEntity.GetInverseVisuals())
                _GameObject.transform.localScale = new Vector3(1, 1, 1);
            else
                _GameObject.transform.localScale = new Vector3(-1, 1, 1);
        }

        _DesiredVelocity = new Vector2(_DirToPlayer.x, 0) * _AIEntity.GetInstanceStats().maxSpeed;
    }

    public override void FixedUpdate()
    {
        _Velocity = _RigidBody.velocity;

        _MaxSpeedChange = _AIEntity.GetInstanceStats().acceleration * Time.deltaTime;
        _Velocity.x = Mathf.MoveTowards(_Velocity.x, _DesiredVelocity.x, _MaxSpeedChange);

        _RigidBody.velocity = _Velocity;
    }

    private void CalculatePlayerTracking()
    {
        float _DisToPlayer = Vector2.Distance(
            new Vector2(_RigidBody.position.x, _RigidBody.position.y),
            new Vector2(_Player.transform.position.x, _Player.transform.position.y));

        if (_DisToPlayer < _AIEntity.GetInstanceStats().attackRange)
        {
            _StateMachine.CurrentState = new AttackPlayer(_AIEntity);
            _RigidBody.velocity = new Vector2(0,0);
        }
    }

    public override void Exit()
    {
        _Animator.SetBool(_AIEntity.ANIMHASH_MOVING, false);
    }
}
