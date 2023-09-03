using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : MonoBehaviour, IDamagable, IObjectPoolItem
{
    [SerializeField] EntityStats entityStats;
    [SerializeField] private int deathSouls;

    [SerializeField] private bool isFly;
    public bool GetIsFly() => isFly;

    [SerializeField] private bool isSuicide;
    public bool GetIsSuicide() => isSuicide;

    [SerializeField] private bool inverseVisuals;
    public bool GetInverseVisuals() => inverseVisuals;
    
    [SerializeField] private float nextWaypointDis;
    public float GetWaypointDis() => nextWaypointDis;

    private GameObject _Player;
    public GameObject GetPlayer() => _Player;

    private StateMachine _StateMachine = new();
    public StateMachine GetStateMachine() => _StateMachine;

    private Animator _Animator;
    public Animator GetAnimator() => _Animator;

    private Rigidbody2D _Rigidbody;
    public Rigidbody2D GetRigidbody2D() => _Rigidbody;

    private Seeker _Seeker;
    public Seeker GetSeeker() => _Seeker;

    private Currency _PlayerCurrency;
    private IDamagable _PlayerDamage;
    private ZoneSpawner _ZoneSpawner;

    [SerializeField] private AnimationAttackHook _AnimAttackhook;

    #region Instance Stats

    private InstanceEntityStats _InstanceStats;
    public InstanceEntityStats GetInstanceStats() => _InstanceStats;

    #endregion

    #region Anim Hashes

    public readonly int ANIMHASH_ATTACK = Animator.StringToHash("Attack");
    public readonly int ANIMHASH_HIT = Animator.StringToHash("Hit");
    public readonly int ANIMHASH_DEAD = Animator.StringToHash("IsDead");
    public readonly int ANIMHASH_MOVING = Animator.StringToHash("IsMoving");

    #endregion

    private void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _PlayerCurrency = _Player.GetComponent<Currency>();
        _PlayerDamage = _Player.GetComponent<IDamagable>();

        _Seeker = GetComponent<Seeker>();
        _Animator = GetComponentInChildren<Animator>();
        _Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _InstanceStats = new InstanceEntityStats(entityStats);
        _AnimAttackhook.Attack = PerformAttack;
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
            Death();
        else
            _Animator.SetTrigger(ANIMHASH_HIT);
    }

    public void Death()
    {
        //_PlayerCurrency?.AddCurrency(deathSouls);
        _ZoneSpawner?.RemoveEnemyFromZone(gameObject);
        _Animator.SetBool(ANIMHASH_DEAD, true);
        _Animator.SetBool(ANIMHASH_MOVING, false);
        _Animator.SetBool(ANIMHASH_ATTACK, false);
        enabled = false;

        if(GetIsSuicide())
            gameObject.SetActive(false);
    }

    private void PerformAttack()
    {
        _PlayerDamage.Damage(GetInstanceStats().attackDamage);

        if (GetIsSuicide())
            Death();
        else
            _Animator.SetTrigger(ANIMHASH_ATTACK);
    }

    #endregion

    #region IObjectPool

    public GameObject GetGameObject() => gameObject;

    public void Reset(ObjectPoolItemData _NextItemResetData)
    {
        enabled = true;
        gameObject.SetActive(true);
        transform.position = _NextItemResetData.spawnPosition;
        _ZoneSpawner = _NextItemResetData.spawner.GetComponent<ZoneSpawner>();
        _InstanceStats = new InstanceEntityStats(entityStats);
        
        if (isFly)
            _StateMachine.CurrentState = new TrackPlayerFly(this);
        else
            _StateMachine.CurrentState = new TrackPlayer(this);
        _Animator.SetBool(ANIMHASH_DEAD, false);
    }

    public bool IsActive() => gameObject.activeSelf;

    #endregion
}
