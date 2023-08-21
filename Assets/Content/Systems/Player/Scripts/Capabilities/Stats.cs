using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamagable
{
    [SerializeField] private EntityStats baseStats;

    private InstanceEntityStats _InstanceStats;

    private void Start()
    {
        _InstanceStats = new InstanceEntityStats(baseStats);
    }

    public void Death()
    {
        //Spawn Death Effect
        //Spawn Gameover UI
        gameObject.SetActive(false);
    }

    public void Damage(int _Damage)
    {
        _InstanceStats.Health -= _Damage;
        if(_InstanceStats.Health <= 0)
            Death();
    }
}
