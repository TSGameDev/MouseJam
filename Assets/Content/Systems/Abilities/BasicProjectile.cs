using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour, IObjectPoolItem
{
    [SerializeField] private float projectileMaxDistance;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;
    [SerializeField] private bool isAreaEffect;
    [SerializeField] private float areaEffectRadius;
    [SerializeField] private LayerMask enemyLayer;

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
        {
            if (isAreaEffect)
                DamageArea();
            else
                gameObject.SetActive(false);
        }
    }

    public void DamageArea()
    {
        Collider2D[] _enemiesInRange = Physics2D.OverlapCircleAll(transform.position, areaEffectRadius, enemyLayer);
        foreach(Collider2D enemy in _enemiesInRange)
        {
            IDamagable _TargetDamamge = enemy.gameObject.GetComponent<IDamagable>();
            _TargetDamamge?.Damage(projectileDamage);
        }
        // Spawn AOE effect
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
        if (collision.collider.CompareTag("Player"))
            return;
        if (isAreaEffect)
        {
            DamageArea();
            return;
        }

        IDamagable _TargetDamage = collision.gameObject.GetComponent<IDamagable>();
        _TargetDamage?.Damage(projectileDamage);
        //spawn hit effect
        gameObject.SetActive(false);
    }
}
