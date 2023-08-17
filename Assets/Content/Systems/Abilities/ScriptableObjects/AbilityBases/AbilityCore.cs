using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityCore : ScriptableObject, IAbility
{
    public virtual void Perform(Vector3 _SpawnPos, Vector3 _CharacterLookDir)
    {
        throw new System.NotImplementedException();
    }

    public virtual void SetUp()
    {
        throw new System.NotImplementedException();
    }
}
