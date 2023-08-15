using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour, IObjectPoolItem
{
    [SerializeField][Range(1,40)] private float projectileMaxDistance = 10f;
    [SerializeField][Range(1, 10)] private float projectileSpeed = 2f;
    //[SerializeField][Range(1, 100)] private float projectileDamage = 10f;

    private Vector3 _MovementDir;
    private float _ProjectileTravelDistance = 0f;

    private void Update()
    {
        Vector3 _FrameMovementVec = _MovementDir * projectileSpeed * Time.deltaTime;
        float _FrameMovementFloat = _FrameMovementVec.magnitude;
        transform.position += _FrameMovementVec;
        _ProjectileTravelDistance += _FrameMovementFloat;
        
        if(_ProjectileTravelDistance >= projectileMaxDistance)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public GameObject GetGameObject() => gameObject;

    public void Reset(ObjectPoolItemData _NextItemResetData)
    {
        _ProjectileTravelDistance = 0;
        transform.position = _NextItemResetData.spawnPosition;
        _MovementDir = _NextItemResetData.characterLookDir;
        gameObject.SetActive(true);
    }
}
