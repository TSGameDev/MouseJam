using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool _OnGround;
    private float _Friction;

    public bool GetOnGround() => _OnGround;
    public float GetFriction() => _Friction;

    private void EvaluateCollision(Collision2D collision)
    {
        for(int i = 0; i < collision.contactCount; i++)
        {
            Vector2 _ContactNormal = collision.GetContact(i).normal;
            _OnGround |= _ContactNormal.y >= 0.9f;
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        PhysicsMaterial2D _CollisionPhysicsMat = collision.rigidbody.sharedMaterial;
        _Friction = _CollisionPhysicsMat != null ? _CollisionPhysicsMat.friction : 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Terrain"))
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Terrain"))
        {
            EvaluateCollision(collision);
            RetrieveFriction(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _OnGround = false;
        _Friction = 0f;
    }
}
