using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Increase Effect", menuName = "Status Effects/New Stat Increase")]
public class StatIncreaseStatusEffectSO : StatusEffectSOBase
{
    [SerializeField] private int maxHealthIncrease;
    [SerializeField] private int speedIncrease;
    [SerializeField] private int jumpHeightIncrease;
    [SerializeField] private int maxAirJumpsIncrease;
    [SerializeField] private int cooldownReductionIncrease;

    public override void RemoveStatusEffect(IEffectable _Target)
    {
        Debug.Log("This is nothing to remove for this effect");
    }

    public override void ApplyStatusEffect(IEffectable _Target)
    {
        InstanceEntityStats _PlayerStats = _Target.GetStats();

        IncreaseMaxHealth(_PlayerStats, maxHealthIncrease);
        IncreaseSpeed(_PlayerStats, speedIncrease);
        IncreaseJumpHeight(_PlayerStats, jumpHeightIncrease);
        IncreaseMaxJumps(_PlayerStats, maxAirJumpsIncrease);
        IncreaseCooldownReduction(_PlayerStats, cooldownReductionIncrease);

        _Target.SetStats(_PlayerStats);
    }

    private void IncreaseMaxHealth(InstanceEntityStats _PlayerStats, int _StatIncreases)
    {
        _PlayerStats.maxHealth += _StatIncreases;
        _PlayerStats.Health += _StatIncreases;
    }

    private void IncreaseSpeed(InstanceEntityStats _PlayerStats, int _StatIncreases) => 
        _PlayerStats.maxSpeed += _StatIncreases;
    private void IncreaseJumpHeight(InstanceEntityStats _PlayerStats, int _StatIncreases) => 
        _PlayerStats.jumpHeight += _StatIncreases;
    private void IncreaseMaxJumps(InstanceEntityStats _PlayerStats, int _StatIncreases) => 
        _PlayerStats.maxAirJumps += _StatIncreases;
    private void IncreaseCooldownReduction(InstanceEntityStats _PlayerStats, int _StatIncreases) => 
        _PlayerStats.cooldownReduction += _StatIncreases;
}
