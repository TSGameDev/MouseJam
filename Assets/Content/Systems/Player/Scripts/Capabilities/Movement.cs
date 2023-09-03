using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputManagerBase controls;

    private float maxSpeed;
    private float maxAcceleration;
    private float maxAirAcceleration;

    public void SetUpDependancies(InstanceEntityStats _EntityStats)
    {
        maxSpeed = _EntityStats.maxSpeed;
        maxAcceleration = _EntityStats.acceleration;
        maxAirAcceleration = _EntityStats.airAcceleration;
    }

    private Vector2 _Direction;
    private Vector2 _DesiredVelocity;
    private Vector2 _Velocity;

    private Rigidbody2D _RB;
    private GroundCheck _GroundCheck;

    private float _MaxSpeedChange;
    private float _Acceleration;
    private bool _OnGround;

    private void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        _GroundCheck = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        _Direction.x = controls.RetrieveHorizontalMovement();


        //Add Player visual flip and have the fire pos transform flip to a different point based on playing direction.
        if (_Direction.x > 0)
        {
            //moving right
            transform.localScale = new Vector3(1,1,1);
        }
        else if(_Direction.x < 0)
        {
            //moving left
            //Flips visuals to face left
            transform.localScale = new Vector3(-1, 1, 1);
        }

        _DesiredVelocity = new Vector2(_Direction.x, 0) * Mathf.Max(maxSpeed - _GroundCheck.GetFriction(), 0f);
    }

    private void FixedUpdate()
    {
        _OnGround = _GroundCheck.GetOnGround();
        _Velocity = _RB.velocity;

        _Acceleration = _OnGround ? maxAcceleration : maxAirAcceleration;
        _MaxSpeedChange = _Acceleration * Time.deltaTime;
        _Velocity.x = Mathf.MoveTowards(_Velocity.x, _DesiredVelocity.x, _MaxSpeedChange);

        _RB.velocity = _Velocity;
    }
}
