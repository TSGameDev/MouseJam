using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : MonoBehaviour, IDamagable
{
    [SerializeField] GameObject player;
    [SerializeField] EntityStats entityStats;

    private InstanceEntityStats _InstanceStats;
    public InstanceEntityStats GetInstanceStats() => _InstanceStats;

    private StateMachine _StateMachine = new();

    public readonly int ANIMHASH_WALK = Animator.StringToHash("Idle");


    private void Start()
    {
        _InstanceStats = new InstanceEntityStats(entityStats);

        Animator _Animator = GetComponent<Animator>();
        Rigidbody2D _Rigidbody = GetComponent<Rigidbody2D>();
        
        _StateMachine.CurrentState = new TrackPlayer(gameObject, _StateMachine, _Animator, _Rigidbody, this, player);
    }

    private void Update()
    {
        _StateMachine.UpdateCurrentState();
    }

    private void FixedUpdate()
    {
        _StateMachine.FixedUpdateCurrentState();
    }

    #region IDamage

    public void Damage(int _Damage)
    {
        _InstanceStats.Health -= _Damage;
        if (_InstanceStats.Health <= 0)
            Destroy(gameObject);

        //Update to just despawn since spawning the enemies will be object pooled.
    }

    #endregion
}
