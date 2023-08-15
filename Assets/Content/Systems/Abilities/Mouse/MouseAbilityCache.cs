using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAbilityCache : MonoBehaviour, IAbilityCache
{
    [Header("General")]
    [SerializeField] private InputManagerBase controls;

    [Header("Normal Attack")]
    [SerializeField] private GameObject normalAttackProjectile;
    [SerializeField] private Transform normalAttackSpawnPos;

    string NORMAL_ATTACK_OBJECTPOOL = "MouseNormalAttack";

    private void Start()
    {
        ObjectPool.CreateObjectPool(NORMAL_ATTACK_OBJECTPOOL, normalAttackProjectile.GetComponent<IObjectPoolItem>(), 20);
    }

    private void Update()
    {
        if (controls.RetrieveNormalAttack())
            PerformNormalAttack();
    }

    public void PerformAbility1()
    {
        throw new System.NotImplementedException();
    }

    public void PerformAbility2()
    {
        throw new System.NotImplementedException();
    }

    public void PerformAbility3()
    {
        throw new System.NotImplementedException();
    }

    public void PerformNormalAttack() => ObjectPool.SpawnItem(NORMAL_ATTACK_OBJECTPOOL, new ObjectPoolItemData(normalAttackSpawnPos.position, transform.right));
}
