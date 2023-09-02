using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : MonoBehaviour, IDamagable, IObjectPoolItem
{
    [SerializeField] GameObject player;
    [SerializeField] EntityStats entityStats;

    [SerializeField] private int deathSouls;

    private InstanceEntityStats _InstanceStats;
    public InstanceEntityStats GetInstanceStats() => _InstanceStats;

    private StateMachine _StateMachine = new();
    private Animator _Animator;
    private Rigidbody2D _Rigidbody;
    private ZoneSpawner _ZoneSpawner;
    private Currency _PlayerCurrency;

    public readonly int ANIMHASH_WALK = Animator.StringToHash("Idle");


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _PlayerCurrency = player.GetComponent<Currency>();

        _InstanceStats = new InstanceEntityStats(entityStats);

        _Animator = GetComponent<Animator>();
        _Rigidbody = GetComponent<Rigidbody2D>();
        
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
        {
            _PlayerCurrency.AddCurrency(deathSouls);
            _ZoneSpawner.RemoveEnemyFromZone(gameObject);
            gameObject.SetActive(false);
        }
    }

    #endregion

    #region IObjectPool

    public GameObject GetGameObject() => gameObject;

    public void Reset(ObjectPoolItemData _NextItemResetData)
    {
        _InstanceStats = new InstanceEntityStats(entityStats);
        _StateMachine.CurrentState = new TrackPlayer(gameObject, _StateMachine, _Animator, _Rigidbody, this, player);
        transform.position = _NextItemResetData.spawnPosition;
        _ZoneSpawner = _NextItemResetData.spawner.GetComponent<ZoneSpawner>();
        gameObject.SetActive(true);
    }

    public bool IsActive() => gameObject.activeSelf;

    #endregion
}
