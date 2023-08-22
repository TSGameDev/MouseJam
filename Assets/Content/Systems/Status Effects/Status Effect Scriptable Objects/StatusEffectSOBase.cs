using UnityEngine;

public class StatusEffectSOBase : ScriptableObject, IStatusEffect
{
    [SerializeField] private string effectName;
    [SerializeField][TextArea] private string effectDescription;
    [SerializeField] private bool isInstantEffect;
    [SerializeField] private bool isPassive;
    [SerializeField] private int maxTicks;
    [SerializeField] private TickTime tickIntervals;

    public virtual string GetEffectName() => effectName;
    public virtual string GetEffectDescription() => effectDescription;
    public virtual bool GetIsInstant() => isInstantEffect;
    public virtual int GetMaxTick() => maxTicks;
    public virtual TickTime GetTickTime() => tickIntervals;
    public virtual bool GetIsPassive() => isPassive;

    public virtual void RemoveStatusEffect(IEffectable _Target)
    {
        throw new System.NotImplementedException();
    }

    public virtual void ApplyStatusEffect(IEffectable _Target)
    {
        throw new System.NotImplementedException();
    }
}
