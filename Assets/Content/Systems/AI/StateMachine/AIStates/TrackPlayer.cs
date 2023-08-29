using UnityEngine;

public class TrackPlayer : State
{
    private GameObject _Player;

    private Vector2 _DesiredVelocity;
    private Vector2 _Velocity;
    private float _MaxSpeedChange;

    public TrackPlayer(GameObject GameObject, StateMachine StateMachine, Animator Animator, Rigidbody2D RigidBody, AIEntity AIEntity, GameObject Player)
        : base(GameObject, StateMachine, Animator, RigidBody, AIEntity)
    {
        _Player = Player;
    }

    public override void Enter()
    {
        Debug.Log("Enter Tracking State");
    }

    public override void Update()
    {
        CalculatePlayerTracking();

        Vector3 _DirToPlayer = (_Player.transform.position - _GameObject.transform.position).normalized;

        if (_DirToPlayer.x > 0)
            _GameObject.transform.localScale = new Vector3(1, 2, 1);
        else if (_DirToPlayer.x < 0)
            _GameObject.transform.localScale = new Vector3(-1, 2, 1);

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
            new Vector2(_GameObject.transform.position.x, _GameObject.transform.position.y),
            new Vector2(_Player.transform.position.x, _Player.transform.position.y));

        if (_DisToPlayer < _AIEntity.GetInstanceStats().attackRange)
        {
            _StateMachine.CurrentState = new AttackPlayer(_GameObject, _StateMachine, _Animator, _RigidBody, _AIEntity, _Player);
            _RigidBody.velocity = new Vector2(0,0);
        }
    }
}
