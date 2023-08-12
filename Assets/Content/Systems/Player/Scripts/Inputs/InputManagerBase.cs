using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class InputManagerBase : ScriptableObject
{
    public abstract float RetrieveHorizontalMovement();
    public abstract bool RetrieveJump();
    public abstract bool RetrieveFallThrough();
}
