using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour, IDamagable, IEffectable
{
    [SerializeField] private EntityStats baseStats;

    private List<IStatusEffect> _ActivePassives = new();

    private Movement _MovementComponent;
    private Jump _JumpComponent;
    private AbilityCache _AbilityCache;

    private InstanceEntityStats _InstanceStats;

    private void Awake()
    {
        _MovementComponent = GetComponent<Movement>();
        _JumpComponent = GetComponent<Jump>();
        _AbilityCache = GetComponent<AbilityCache>();
    }

    private void Start()
    {
        _InstanceStats = new InstanceEntityStats(baseStats);

        UIHUDManager.Instance.SetUpHealthUI(_InstanceStats.maxHealth, _InstanceStats.Health);

        _MovementComponent.SetUpDependancies(_InstanceStats);
        _JumpComponent.SetUpDependancies(_InstanceStats);
        _AbilityCache.SetupDependancies(_InstanceStats);
    }

    public void Death()
    {
        //Spawn Death Effect
        //trigger death event
        gameObject.SetActive(false);
    }

    public void Damage(int _Damage)
    {
        _InstanceStats.Health -= _Damage;

        UIHUDManager.Instance.UpdateHealthSlider(_InstanceStats.Health);

        if(_InstanceStats.Health <= 0)
            Death();
    }

    public List<IStatusEffect> GetActivePassives() => _ActivePassives;

    public void Effect(IStatusEffect[] potionStatusEffects)
    {
        throw new System.NotImplementedException();
    }

    public void SetStats(InstanceEntityStats _NewStats)
    {
        _InstanceStats = _NewStats;
        _MovementComponent.SetUpDependancies(_InstanceStats);
        _JumpComponent.SetUpDependancies(_InstanceStats);
    }
    
    public InstanceEntityStats GetStats() => _InstanceStats;

    public EntityStats GetBaseStats() => baseStats;
}
