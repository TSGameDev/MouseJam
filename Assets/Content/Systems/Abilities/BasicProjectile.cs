using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour, IObjectPoolItem
{
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private AudioClip hitEffectClip;
    [SerializeField] private float projectileMaxDistance;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;
    [SerializeField] private bool isAreaEffect;
    [SerializeField] private float areaEffectRadius;
    [SerializeField] private LayerMask enemyLayer;

    private string objectPoolHitEffectName;
    private float _ProjectileTravelDistance = 0f;
    private Vector3 _MovementDir;

    public GameObject GetGameObject() => gameObject;

    private void Start()
    {
        objectPoolHitEffectName = gameObject.name + " Hit Effect";
        if(!ObjectPool.CheckObjectPool(objectPoolHitEffectName))
            ObjectPool.CreateObjectPool(objectPoolHitEffectName, hitEffect.GetComponent<IObjectPoolItem>(), 5);
    }

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
        ObjectPool.SpawnItem(objectPoolHitEffectName, new ObjectPoolItemData(transform.position));
        AudioManager.Instance.PlayOneShot(hitEffectClip);
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
        ObjectPool.SpawnItem(objectPoolHitEffectName, new ObjectPoolItemData(transform.position));
        AudioManager.Instance.PlayOneShot(hitEffectClip);
        gameObject.SetActive(false);
    }

    public bool IsActive() => gameObject.activeSelf;
}
