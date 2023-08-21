using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputManagerBase controls;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;

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
        if(_Direction.x > 0)
        {
            //moving right
            transform.localScale = new Vector3(1,2,1);
        }
        else if(_Direction.x < 0)
        {
            //moving left
            //Flips visuals to face left
            transform.localScale = new Vector3(-1, 2, 1);
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
