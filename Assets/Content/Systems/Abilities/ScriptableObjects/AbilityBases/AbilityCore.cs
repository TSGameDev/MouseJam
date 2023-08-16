using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityCore : ScriptableObject, IAbility
{
    public virtual void Perform()
    {
        throw new System.NotImplementedException();
    }
}
