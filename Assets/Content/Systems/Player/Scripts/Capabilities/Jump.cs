using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputManagerBase controls;

    private float jumpHeight;
    private int maxAirJumps;
    private float downwardGravityMultiplier;
    private float upwardGravityMultiplier;
    public void SetUpDependancies(InstanceEntityStats _EntityStats)
    {
        jumpHeight = _EntityStats.jumpHeight;
        maxAirJumps = _EntityStats.maxAirJumps;
        downwardGravityMultiplier = _EntityStats.downwardGravitityMuliplier;
        upwardGravityMultiplier = _EntityStats.upwardGravitiyMuliplier; 
    }

    private Rigidbody2D _RB;
    private GroundCheck _GroundCheck;
    
    private Vector2 _Velocity;

    private int _JumpPhase;
    private bool _IsGrounded;
    private bool _ToJump;

    private float DEFAULT_GRAVITY_SCALE = 1f;

    private void Awake()
    {
        _RB = GetComponent<Rigidbody2D>();
        _GroundCheck = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        _ToJump |= controls.RetrieveJump();
    }

    private void FixedUpdate()
    {
        _IsGrounded = _GroundCheck.GetOnGround();
        _Velocity = _RB.velocity;

        if(_IsGrounded)
        {
            _JumpPhase = 0;
        }

        if (_ToJump)
        {
            _ToJump = false;
            JumpAction();
        }

        if(_RB.velocity.y > 0)
        {
            _RB.gravityScale = upwardGravityMultiplier;
        }
        else if(_RB.velocity.y < 0)
        {
            _RB.gravityScale = downwardGravityMultiplier;
        }
        else
        {
            _RB.gravityScale = DEFAULT_GRAVITY_SCALE;
        }

        _RB.velocity = _Velocity;
    }

    private void JumpAction()
    {
        if (_IsGrounded || _JumpPhase < maxAirJumps)
        {
            _JumpPhase++;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);
            if (_Velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - _Velocity.y, 0f);
            }
            _Velocity.y += jumpSpeed;
        }
    }
}
