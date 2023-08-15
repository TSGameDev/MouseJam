using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WeaponAimToMouse : MonoBehaviour
{
    [SerializeField] InputManagerBase controls;

    private void Update()
    {
        RotateWeapon();
    }

    private void RotateWeapon()
    {
        Vector3 _WorldMousePos = Camera.main.ScreenToWorldPoint(controls.RetrieveMousePos());
        Vector3 _AimDir = (_WorldMousePos - transform.position).normalized;

        float _WeaponAngleEuler = Mathf.Atan2(_AimDir.y, _AimDir.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, _WeaponAngleEuler);
    }
}
