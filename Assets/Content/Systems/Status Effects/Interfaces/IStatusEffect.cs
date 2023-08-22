using System;

[Serializable]
public enum TickTime
{
    EveryTick,
    Every5Tick
}

public interface IStatusEffect
{
    public TickTime GetTickTime();
    public int GetMaxTick();
    public bool GetIsInstant();
    public string GetEffectName();
    public string GetEffectDescription();
    public bool GetIsPassive();

    public void ApplyStatusEffect(IEffectable _Target);
    public void RemoveStatusEffect(IEffectable _Target);
}