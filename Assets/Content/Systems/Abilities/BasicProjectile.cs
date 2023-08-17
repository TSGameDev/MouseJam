using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour, IObjectPoolItem
{
    [SerializeField] private float projectileMaxDistance;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private bool isAreaEffect;
    [SerializeField] private float areaEffectRadius;

    private float _ProjectileTravelDistance = 0f;
    private Vector3 _MovementDir;

    public GameObject GetGameObject() => gameObject;

    private void Update()
    {
        Vector3 _FrameMovementVec = _MovementDir * projectileSpeed * Time.deltaTime;
        float _FrameMovementFloat = _FrameMovementVec.magnitude;
        transform.position += _FrameMovementVec;
        _ProjectileTravelDistance += _FrameMovementFloat;
        
        if(_ProjectileTravelDistance >= projectileMaxDistance)
            gameObject.SetActive(false);
    }

    public void Reset(ObjectPoolItemData _NextItemResetData)
    {
        _ProjectileTravelDistance = 0;
        transform.position = _NextItemResetData.spawnPosition;
        _MovementDir = _NextItemResetData.characterLookDir;
        gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
