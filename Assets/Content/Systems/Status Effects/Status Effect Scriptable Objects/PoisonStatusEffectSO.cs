using UnityEngine;

[CreateAssetMenu(fileName = "New Poison Effect", menuName = "Status Effects/New Poison Effect")]
public class PoisonStatusEffectSO : StatusEffectSOBase
{
    [Tooltip("The damage deal every tick, if the poison is set to instant it does this damange only once")]
    [SerializeField] private int damagePerTick = 1;
    public override void RemoveStatusEffect(IEffectable _Target)
    {
        Debug.Log("This is nothing to remove for this effect");
    }

    public override void ApplyStatusEffect(IEffectable _Target)
    {
        InstanceEntityStats _NewTargetStats = _Target.GetStats();
        _NewTargetStats.Health -= damagePerTick;
        _Target.SetStats(_NewTargetStats);
    }
}
