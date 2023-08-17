using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Projectile Spawn Ability", menuName = "Abilities/New Projectile Spawn Ability")]
public class SpawnProjectileAbility : AbilityCore
{
    [SerializeField] private GameObject projectilePrefab;

    [Header("Projectile Settings")]
    [SerializeField] private float projectileMaxDistance;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private string projectileObjectpoolKey;

    public override void SetUp()
    {

    }

    public override void Perform()
    {
        base.Perform();
    }
}
