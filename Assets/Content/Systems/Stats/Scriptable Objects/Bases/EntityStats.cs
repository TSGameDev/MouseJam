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
        get
        {
            return health;
        }
    }

    public InstanceEntityStats(EntityStats _BaseStats)
    {
        maxHealth = _BaseStats.MaxHealth;
        health = _BaseStats.MaxHealth;
    }
}

[CreateAssetMenu(fileName = "New Entity Stats", menuName = "Stats/New Entity Stats")]
public class EntityStats : ScriptableObject
{
    [SerializeField] private int maxHealth;
    public int MaxHealth 
    { 
        get { return maxHealth; } 
        private set { maxHealth = value; } 
    }
}
