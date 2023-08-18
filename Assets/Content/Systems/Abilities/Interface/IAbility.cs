using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public void Perform(Transform _SpawnTransform, Vector3 _CharacterLookDir);
    public void SetUp();
}
