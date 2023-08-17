using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public void Perform(Vector3 _SpawnPos, Vector3 _CharacterLookDir);
    public void SetUp();
}
