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

    private bool _NormalAttackOnCooldown = false;
    private float _CurrentNormalAttackCooldown;
    public bool GetNormalAttackCooldown() => _NormalAttackOnCooldown;

    private bool _Ability1OnCooldown = false;
    private float _CurrentAbility1Cooldown;
    public bool GetAbility1Cooldown() => _Ability1OnCooldown;

    private bool _Ability2OnCooldown = false;
    private float _CurrentAbility2Cooldown;
    public bool GetAbility2OnCooldown() => _Ability2OnCooldown;

    private bool _Ability3OnCooldown = false;
    private float _CurrentAbility3Cooldown;
    public bool GetAbility3OnCooldown() => _Ability3OnCooldown;

    private float _CharacterCooldownReduction;
    public void SetupDependancies(InstanceEntityStats _EntityStats)
    {
        _CharacterCooldownReduction = _EntityStats.cooldownReduction;
    }

    #region Animation

    private Animator _Animator;
    private int ANIMHASH_ATTACK = Animator.StringToHash("IsAttacking");

    #endregion

    private Vector3 _CharacterLookDir;

    private void Start()
    {
        _Animator = GetComponent<Animator>();

        normalAtack?.SetUp();
        Ability1?.SetUp();
        Ability2?.SetUp();
        Ability3?.SetUp();

        UIHUDManager.Instance?.SetUpAbilityUI(normalAtack, Ability1, Ability2, Ability3);
    }

    private void Update()
    {
        FlipDirectionVector();
        CheckAbilityTriggers();
        CountdownCooldowns();
    }

    private void FlipDirectionVector()
    {
        if (transform.localScale.x > 0)
            _CharacterLookDir = transform.right;
        else if (transform.localScale.x < 0)
            _CharacterLookDir = -transform.right;
    }

    private void CheckAbilityTriggers()
    {
        if (controls.RetrieveNormalAttack() && !_NormalAttackOnCooldown)
            PerformNormalAttack();

        if (controls.RetrieveAbility1() && !_Ability1OnCooldown)
            PerformAbility1();

        if (controls.RetrieveAbility2() && !_Ability2OnCooldown)
            PerformAbility2();

        if (controls.RetrieveAbility3() && !_Ability3OnCooldown)
            PerformAbility3();

        _Animator.SetBool(ANIMHASH_ATTACK, controls.RetrieveNormalAttackHeld());
    }

    private void CountdownCooldowns()
    {
        if(_CurrentNormalAttackCooldown >= 0)
        {
            _CurrentNormalAttackCooldown -= 1 * Time.deltaTime;
            UIHUDManager.Instance?.UpdateNormalAttackUI(_NormalAttackOnCooldown, _CurrentNormalAttackCooldown);
        }
        else
        {
            _CurrentNormalAttackCooldown = 0;
            _NormalAttackOnCooldown = false;
        }

        if (_CurrentAbility1Cooldown >= 0)
        {
            _CurrentAbility1Cooldown -= 1 * Time.deltaTime;
            UIHUDManager.Instance?.UpdateAbility1UI(_Ability1OnCooldown, _CurrentAbility1Cooldown);
        }
        else
        {
            _CurrentAbility1Cooldown = 0;
            _Ability1OnCooldown = false;
        }

        if (_CurrentAbility2Cooldown >= 0)
        {
            _CurrentAbility2Cooldown -= 1 * Time.deltaTime;
            UIHUDManager.Instance?.UpdateAbility2UI(_Ability2OnCooldown, _CurrentAbility2Cooldown);
        }
        else
        {
            _CurrentAbility2Cooldown = 0;
            _Ability2OnCooldown = false;
        }

        if (_CurrentAbility3Cooldown >= 0)
        {
            _CurrentAbility3Cooldown -= 1 * Time.deltaTime;
            UIHUDManager.Instance?.UpdateAbility3UI(_Ability3OnCooldown, _CurrentAbility3Cooldown);
        }
        else
        {
            _CurrentAbility3Cooldown = 0;
            _Ability3OnCooldown = false;
        }
    }

    private void PerformNormalAttack()
    {
        if (isNormalProjectile)
            normalAtack?.Perform(projectileFirePoint, _CharacterLookDir);
        else
            normalAtack?.Perform(transform, _CharacterLookDir);

        _CurrentNormalAttackCooldown = normalAtack.GetAbilityCooldown() * ((100 - _CharacterCooldownReduction) / 100);
        _NormalAttackOnCooldown = true;
    }

    private void PerformAbility1()
    {
        if (isAbility1Projectile)
            Ability1?.Perform(projectileFirePoint, _CharacterLookDir);
        else
            Ability1?.Perform(transform, _CharacterLookDir);

        _CurrentAbility1Cooldown = Ability1.GetAbilityCooldown() * ((100 - _CharacterCooldownReduction) / 100);
        _Ability1OnCooldown = true;
    }
    
    private void PerformAbility2()
    {
        if (isAbility2Projectile)
            Ability2?.Perform(projectileFirePoint, _CharacterLookDir);
        else
            Ability2?.Perform(transform, _CharacterLookDir);

        _CurrentAbility2Cooldown = Ability2.GetAbilityCooldown() * ((100 - _CharacterCooldownReduction)/100);
        _Ability2OnCooldown = true;
    }

    private void PerformAbility3()
    {
        if (isAbility3Projectile)
            Ability3?.Perform(projectileFirePoint, _CharacterLookDir);
        else
            Ability3?.Perform(transform, _CharacterLookDir);

        _CurrentAbility3Cooldown = Ability3.GetAbilityCooldown() * ((100 - _CharacterCooldownReduction) / 100);
        _Ability3OnCooldown = true;
    }
}
