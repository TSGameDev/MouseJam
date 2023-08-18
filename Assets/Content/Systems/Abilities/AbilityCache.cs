using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCache : MonoBehaviour
{
    [SerializeField] private InputManagerBase controls;
    [SerializeField] private Transform projectileFirePoint;

    [SerializeField] private AbilityCore normalAtack;
    [SerializeField] private bool isNormalProjectile = false;
    [SerializeField] private AbilityCore Ability1;
    [SerializeField] private bool isAbility1Projectile = false;
    [SerializeField] private AbilityCore Ability2;
    [SerializeField] private bool isAbility2Projectile = false;
    [SerializeField] private AbilityCore Ability3;
    [SerializeField] private bool isAbility3Projectile = false;

    private void Start()
    {
        normalAtack?.SetUp();
        Ability1?.SetUp();
        Ability2?.SetUp();
        Ability3?.SetUp();
    }

    private void Update()
    {
        if (controls.RetrieveNormalAttack())
            PerformNormalAttack();

        if (controls.RetrieveAbility1())
            PerformAbility1();

        if (controls.RetrieveAbility2())
            PerformAbility2();

        if (controls.RetrieveAbility3())
            PerformAbility3();
    }

    private void PerformNormalAttack()
    {
        if (isNormalProjectile)
            normalAtack?.Perform(projectileFirePoint, transform.right);
        else
            normalAtack?.Perform(transform, transform.right);
    }

    private void PerformAbility1()
    {
        if (isAbility1Projectile)
            Ability1?.Perform(projectileFirePoint, transform.right);
        else
            Ability1?.Perform(transform, transform.right);
    }
    private void PerformAbility2()
    {
        if (isAbility2Projectile)
            Ability2?.Perform(projectileFirePoint, transform.right);
        else
            Ability2?.Perform(transform, transform.right);
    }

    private void PerformAbility3()
    {
        if (isAbility3Projectile)
            Ability3?.Perform(projectileFirePoint, transform.right);
        else
            Ability3?.Perform(transform, transform.right);
    }
}
