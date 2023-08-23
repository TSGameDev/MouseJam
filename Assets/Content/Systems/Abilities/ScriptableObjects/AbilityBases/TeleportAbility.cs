using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Teleport Ability", menuName = "Abilities/New Teleport Ability")]
public class TeleportAbility : AbilityCore
{
    [SerializeField] private float teleportDistance;
    [SerializeField] private AbilityCore[] BeginTeleport;
    [SerializeField] private AbilityCore[] EndTeleport;

    public override void SetUp()
    {
    }

    public override void Perform(Transform _SpawnTransform, Vector3 _CharacterLookDir)
    {
        if(BeginTeleport.Length > 0)
        {
            foreach (AbilityCore ability in BeginTeleport)
            {
                ability.SetUp();
                ability.Perform(_SpawnTransform, _CharacterLookDir);
            }
        }

        _SpawnTransform.position += _CharacterLookDir * teleportDistance;

        if (EndTeleport.Length > 0)
        {
            foreach (AbilityCore ability in EndTeleport)
            {
                ability.SetUp();
                ability.Perform(_SpawnTransform, _CharacterLookDir);
            }
        }
    }
}
