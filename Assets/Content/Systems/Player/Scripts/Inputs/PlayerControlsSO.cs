using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Controls", menuName = "Inputs/Player Controls")]
public class PlayerControlsSO : InputManagerBase
{
    private PlayerActions _PlayerActions;
    private bool normalAttackheld = false;

    private void OnEnable()
    {
        _PlayerActions = new();
        _PlayerActions.Enable();
        _PlayerActions.Game.Enable();

        _PlayerActions.Game.NormalAttack.started+= ctx => normalAttackheld = true;
        _PlayerActions.Game.NormalAttack.performed += ctx => normalAttackheld = true;
        _PlayerActions.Game.NormalAttack.canceled += ctx => normalAttackheld = false;
    }

    private void OnDisable()
    {
        _PlayerActions.Disable();
        _PlayerActions.Game.Disable();
    }

    public override float RetrieveHorizontalMovement()
    {
        return _PlayerActions.Game.Movement.ReadValue<float>();
    }

    public override bool RetrieveJump()
    {
        return _PlayerActions.Game.Jump.WasPerformedThisFrame();
    }

    public override bool RetrieveFallThrough()
    {
        return _PlayerActions.Game.FallThrough.WasPerformedThisFrame();
    }

    public override bool RetrievePause()
    {
        return _PlayerActions.Game.Pause.WasPerformedThisFrame();
    }

    public override Vector2 RetrieveMousePos()
    {
        return _PlayerActions.Game.MousePos.ReadValue<Vector2>();
    }

    public override bool RetrieveNormalAttack()
    {
        return _PlayerActions.Game.NormalAttack.WasPerformedThisFrame();
    }

    public override bool RetrieveNormalAttackHeld() => normalAttackheld;

    public override bool RetrieveAbility1()
    {
        return _PlayerActions.Game.Ability1.WasPerformedThisFrame();
    }

    public override bool RetrieveAbility2()
    {
        return _PlayerActions.Game.Ability2.WasPerformedThisFrame();
    }

    public override bool RetrieveAbility3()
    {
        return _PlayerActions.Game.Ability3.WasPerformedThisFrame();
    }

    public override bool RetrieveInteraction()
    {
        return _PlayerActions.Game.Interact.WasPerformedThisFrame();
    }
}
