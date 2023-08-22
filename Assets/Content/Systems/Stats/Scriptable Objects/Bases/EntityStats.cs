using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InstanceEntityStats
{
    private int maxHealth;
    private int health;
    public int Health
    {
        set
        {
            if (value >= maxHealth)
                health = maxHealth;
            else
                health = value;
        }
        get{ return health; }
    }

    public float maxSpeed;
    public float acceleration;
    public float airAcceleration;
    public float jumpHeight;
    public int maxAirJumps;
    public float downwardGravitityMuliplier;
    public float upwardGravitiyMuliplier;

    public InstanceEntityStats(EntityStats _BaseStats)
    {
        maxHealth = _BaseStats.GetMaxHealth();
        health = _BaseStats.GetMaxHealth();
        maxSpeed = _BaseStats.GetMaxSpeed();
        acceleration = _BaseStats.GetAcceleration();
        airAcceleration = _BaseStats.GetAirAcceleration();
        jumpHeight = _BaseStats.GetJumpHeight();
        maxAirJumps = _BaseStats.GetAirJumps();
        downwardGravitityMuliplier = _BaseStats.GetDownwardGravityMulipliter();
        upwardGravitiyMuliplier = _BaseStats.GetUpwardGravityMulipliter();
    }
}

[CreateAssetMenu(fileName = "New Entity Stats", menuName = "Stats/New Entity Stats")]
public class EntityStats : ScriptableObject
{
    [SerializeField][Range(1,300)] private int baseMaxHealth;
    public int GetMaxHealth() => baseMaxHealth;

    [SerializeField][Range(1, 100)] private float baseMaxSpeed;
    public float GetMaxSpeed() => baseMaxSpeed;
    
    [SerializeField][Range(1, 100)] private float baseAcceleration;
    public float GetAcceleration() => baseAcceleration;

    [SerializeField][Range(1, 100)] private float baseAirAcceleration;
    public float GetAirAcceleration() => baseAirAcceleration;
    
    [SerializeField][Range(1, 10)] private float baseJumpHeight;
    public float GetJumpHeight() => baseJumpHeight;
    
    [SerializeField][Range(1, 5)] private int baseMaxAirJumps;
    public int GetAirJumps() => baseMaxAirJumps;

    [SerializeField][Range(1, 5)] private float baseDownwardGravityMultiplier;
    public float GetDownwardGravityMulipliter() => baseDownwardGravityMultiplier;

    [SerializeField][Range(1, 5)] private float baseUpwardGravityMultiplier;
    public float GetUpwardGravityMulipliter() => baseUpwardGravityMultiplier;
}
