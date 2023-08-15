using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPoolItem
{
    public GameObject GetGameObject();
    public void Reset(ObjectPoolItemData _NextItemResetData);
}

public struct ObjectPoolItemData
{
    public Vector3 spawnPosition;

    public ObjectPoolItemData(Vector3 _SpawnPosition)
    {
        spawnPosition = _SpawnPosition;
    }
}

