using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect Spawner", menuName = "Abilities/New Effect Spawner")]
public class SpawnEffectAtLocation : AbilityCore
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private float distanceFromPlayer;

    private int NUMBER_OF_EFFECTS = 10;

    public override void SetUp()
    {
        ObjectPool.CreateObjectPool(effectName, effectPrefab.GetComponent<IObjectPoolItem>(), NUMBER_OF_EFFECTS);
    }

    public override void Perform(Transform _SpawnTransform, Vector3 _CharacterLookDir)
    {
        Vector3 _EffectSpawnPos = _SpawnTransform.position + (_SpawnTransform.right * distanceFromPlayer);
        ObjectPool.SpawnItem(effectName, new ObjectPoolItemData(_EffectSpawnPos));
    }
}
