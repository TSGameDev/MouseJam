using System.Collections.Generic;
using UnityEngine;

public interface IEffectable
{
    public List<IStatusEffect> GetActivePassives();
    public void Effect(IStatusEffect[] potionStatusEffects);
    public void SetStats(InstanceEntityStats _NewStats);
    public InstanceEntityStats GetStats();
    public EntityStats GetBaseStats();
}

