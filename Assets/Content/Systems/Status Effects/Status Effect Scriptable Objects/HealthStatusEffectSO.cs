using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Effect", menuName = "TSGameDev/Status Effects/New Heal Effect")]
public class HealthStatusEffectSO : StatusEffectSOBase
{
    [SerializeField] private int healthGain = 10;

    public override void RemoveStatusEffect(IEffectable _Target)
    {
        Debug.Log("This is nothing to remove for this effect");
    }

    public override void ApplyStatusEffect(IEffectable _Target)
    {
        InstanceEntityStats _NewTargetStats = _Target.GetStats();
        _NewTargetStats.Health += healthGain;
        _Target.SetStats(_NewTargetStats);
    }
}
