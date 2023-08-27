using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "New Projectile Spawn Ability", menuName = "Abilities/New Projectile Spawn Ability")]
public class SpawnProjectileAbility : AbilityCore
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private AudioClip projectileSpawnSound;

    private int NUMBER_OF_OBJECTPOOLED_PROJECTILES = 20;

    public override void SetUp()
    {
        ObjectPool.CreateObjectPool(effectName, projectilePrefab.GetComponent<IObjectPoolItem>(), NUMBER_OF_OBJECTPOOLED_PROJECTILES);
    }

    public override void Perform(Transform _SpawnTrans, Vector3 _CharacterLookDir)
    {
        ObjectPool.SpawnItem(effectName, new ObjectPoolItemData(_SpawnTrans.position, _CharacterLookDir));
        AudioManager.Instance.PlayOneShot(projectileSpawnSound);
    }
}
