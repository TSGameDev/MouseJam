using UnityEngine;
using Pathfinding;

public class TrackPlayerFly : State
{
    private Path _Path;
    private int _CurrentPathWaypoint;

    private float _CurrentPathTimer = 0f;
    private float _PathUpdateTime = 0.5f;

    private Vector2 _Velocity;
    private Vector2 _DesiredVelocity;
    private float _MaxSpeedChange;

    public TrackPlayerFly(AIEntity AIEntity) : base(AIEntity) { }

    public override void Enter()
    {
        _Animator.SetBool(_AIEntity.ANIMHASH_ATTACK, false);
        _Animator.SetBool(_AIEntity.ANIMHASH_MOVING, true);
        _Seeker.StartPath(_RigidBody.position, _Player.transform.position, OnPathCompleted);
    }

    public override void Update()
    {
        UpdatePath();

        if(!CheckPath())
        {
            CalculatePlayerVectorDir();
            CalculateWaypointDis();
        }
        CalculatePlayerDis();
    }

    public override void FixedUpdate()
    {
        if (!CheckPath())
            CalculateAISpeed();
    }

    /// <summary>
    /// Function to check the AI path. Returns true is there currently isn't a path or if AI is at the end of its current path.
    /// </summary>
    /// <returns>Bool, if the bool is true it means a path needs to be generated for the AI.</returns>
    private bool CheckPath()
    {
        if (_Path == null)
            return true;

        if (_CurrentPathWaypoint >= _Path.vectorPath.Count)
            return true;
        else
            return false;
    }

    private void UpdatePath()
    {
        if(_CurrentPathTimer < _PathUpdateTime)
        {
            _CurrentPathTimer += 1 * Time.deltaTime;
            return;
        }

        _CurrentPathTimer = 0;
        _Seeker.StartPath(_RigidBody.position, _Player.transform.position, OnPathCompleted);

    }

    private void CalculatePlayerVectorDir()
    {
        Vector2 _DirToWaypoint = ((Vector2)_Path.vectorPath[_CurrentPathWaypoint] - _RigidBody.position).normalized;
        _DesiredVelocity = _DirToWaypoint * _AIEntity.GetInstanceStats().maxSpeed;

        if (_DirToWaypoint.x > 0)
        {
            if (_AIEntity.GetInverseVisuals())
                _GameObject.transform.localScale = new Vector3(-1, 1, 1);
            else
                _GameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_DirToWaypoint.x < 0)
        {
            if (_AIEntity.GetInverseVisuals())
                _GameObject.transform.localScale = new Vector3(1, 1, 1);
            else
                _GameObject.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void CalculateAISpeed()
    {
        _Velocity = _RigidBody.velocity;

        _MaxSpeedChange = _AIEntity.GetInstanceStats().acceleration * Time.deltaTime;
        _Velocity.x = Mathf.MoveTowards(_Velocity.x, _DesiredVelocity.x, _MaxSpeedChange);
        _Velocity.y = Mathf.MoveTowards(_Velocity.y, _DesiredVelocity.y, _MaxSpeedChange);

        _RigidBody.velocity = _Velocity;
    }

    private void CalculateWaypointDis()
    {
        float _DisToWaypoint = Vector2.Distance(_RigidBody.position, _Path.vectorPath[_CurrentPathWaypoint]);
        if (_DisToWaypoint < _AIEntity.GetWaypointDis())
            _CurrentPathWaypoint++;
    }

    private void CalculatePlayerDis()
    {
        float _DisToPlayer = Vector2.Distance(
            new Vector2(_RigidBody.position.x, _RigidBody.position.y),
            new Vector2(_Player.transform.position.x, _Player.transform.position.y));
        if (_DisToPlayer <= _AIEntity.GetInstanceStats().attackRange)
            _StateMachine.CurrentState = new AttackPlayer(_AIEntity);
    }

    private void OnPathCompleted(Path _Path)
    {
        if (_Path.error)
            return;

        this._Path = _Path;
        _CurrentPathWaypoint = 0;
    }

    public override void Exit()
    {
        _Animator.SetBool(_AIEntity.ANIMHASH_MOVING, false);
    }
}
