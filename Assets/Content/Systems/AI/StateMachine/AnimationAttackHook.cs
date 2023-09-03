using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAttackHook : MonoBehaviour
{
    public delegate void OnAttack();
    public OnAttack Attack;

    public void PerformAttack() => Attack.Invoke();
}
