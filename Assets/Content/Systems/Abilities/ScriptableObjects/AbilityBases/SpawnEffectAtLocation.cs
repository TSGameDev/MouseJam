using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect Spawner", menuName = "Abilities/New Effect Spawner")]
public class SpawnEffectAtLocation : AbilityCore
{
    [SerializeField] private GameObject effectPrefab;
    [SerializeField] private AudioClip effectStartSound;
    [SerializeField] private float distanceFromPlayer;
    [SerializeField] private LayerMask playerLayer;

    private int NUMBER_OF_EFFECTS = 10;

    public override void SetUp()
    {
        ObjectPool.CreateObjectPool(effectName, effectPrefab.GetComponent<IObjectPoolItem>(), NUMBER_OF_EFFECTS);
    }

    public override void Perform(Transform _SpawnTransform, Vector3 _CharacterLookDir)
    {
        Vector3 _EffectSpawnPos = new();
        RaycastHit2D hit = Physics2D.Raycast(_SpawnTransform.position, _CharacterLookDir, distanceFromPlayer, ~playerLayer);

        if (hit.collider != null)
            _EffectSpawnPos = hit.point;
        else
            _EffectSpawnPos = _SpawnTransform.position + (_CharacterLookDir * distanceFromPlayer);

        AudioManager.Instance.PlayOneShot(effectStartSound);
        ObjectPool.SpawnItem(effectName, new ObjectPoolItemData(_EffectSpawnPos));
    }
}
