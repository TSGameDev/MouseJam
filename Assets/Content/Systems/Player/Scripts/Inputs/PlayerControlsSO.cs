using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Player Controls", menuName = "Inputs/Player Controls")]
public class PlayerControlsSO : InputManagerBase
{
    private PlayerActions _PlayerActions;

    private void OnEnable()
    {
        _PlayerActions = new();
        _PlayerActions.Enable();
        _PlayerActions.Game.Enable();
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

    public override Vector2 RetrieveMousePos()
    {
        return _PlayerActions.Game.MousePos.ReadValue<Vector2>();
    }
}
