using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InputManagerBase : ScriptableObject
{
    public abstract float RetrieveHorizontalMovement();
    public abstract bool RetrieveJump();
    public abstract bool RetrieveFallThrough();
    public abstract Vector2 RetrieveMousePos();
    public abstract bool RetrieveNormalAttack();
    public abstract bool RetrieveAbility1();
    public abstract bool RetrieveAbility2();
    public abstract bool RetrieveAbility3();
}
