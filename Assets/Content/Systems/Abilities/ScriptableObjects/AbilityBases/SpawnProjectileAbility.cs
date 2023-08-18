using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Projectile Spawn Ability", menuName = "Abilities/New Projectile Spawn Ability")]
public class SpawnProjectileAbility : AbilityCore
{
    [SerializeField] private GameObject projectilePrefab;

    [Header("Projectile Settings")]
    [SerializeField] private string projectileName;

    private int NUMBER_OF_OBJECTPOOLED_PROJECTILES = 20;

    public override void SetUp()
    {
        ObjectPool.CreateObjectPool(projectileName, projectilePrefab.GetComponent<IObjectPoolItem>(), NUMBER_OF_OBJECTPOOLED_PROJECTILES);
    }

    public override void Perform(Transform _SpawnTrans, Vector3 _CharacterLookDir)
    {
        ObjectPool.SpawnItem(projectileName, new ObjectPoolItemData(_SpawnTrans.position, _CharacterLookDir));
    }
}
