using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCache : MonoBehaviour
{
    [SerializeField] private InputManagerBase controls;

    [SerializeField] private AbilityCore normalAtack;
    [SerializeField] private AbilityCore Ability1;
    [SerializeField] private AbilityCore Ability2;
    [SerializeField] private AbilityCore Ability3;

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

    private void PerformNormalAttack() => normalAtack?.Perform();

    private void PerformAbility1() => Ability1?.Perform();

    private void PerformAbility2() => Ability2?.Perform();

    private void PerformAbility3() => Ability3?.Perform();
}
