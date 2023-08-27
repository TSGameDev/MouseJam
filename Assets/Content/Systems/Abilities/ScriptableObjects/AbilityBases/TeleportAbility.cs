using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Teleport Ability", menuName = "Abilities/New Teleport Ability")]
public class TeleportAbility : AbilityCore
{
    [SerializeField] private GameObject teleportEffect;
    [SerializeField] private AudioClip teleportSound;
    [SerializeField] private float teleportDistance;
    [SerializeField] private LayerMask terrainLayerMask;
    [SerializeField] private AbilityCore[] BeginTeleport;
    [SerializeField] private AbilityCore[] EndTeleport;

    public override void SetUp()
    {
        ObjectPool.CreateObjectPool(effectName, teleportEffect.GetComponent<IObjectPoolItem>(), 4);
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

        ObjectPool.SpawnItem(effectName, new ObjectPoolItemData(_SpawnTransform.position));

        RaycastHit2D hit = Physics2D.Raycast(_SpawnTransform.position, _CharacterLookDir, teleportDistance, terrainLayerMask);
        if(hit.collider != null)
        {
            _SpawnTransform.position = hit.point - (hit.normal * 0.5f);
        }
        else
            _SpawnTransform.position += _CharacterLookDir * teleportDistance;


        AudioManager.Instance.PlayOneShot(teleportSound);
        ObjectPool.SpawnItem(effectName, new ObjectPoolItemData(_SpawnTransform.position));

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
