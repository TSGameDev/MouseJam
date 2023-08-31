using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectPoolItem
{
    public GameObject GetGameObject();
    public bool IsActive();
    public void Reset(ObjectPoolItemData _NextItemResetData);
}

public struct ObjectPoolItemData
{
    public Vector3 spawnPosition;
    public Vector3 characterLookDir;
    public GameObject spawner;

    public ObjectPoolItemData(Vector3 _SpawnPosition = new(), Vector3 _CharacterLookDir = new(), GameObject _Spawner = null)
    {
        spawnPosition = _SpawnPosition;
        characterLookDir = _CharacterLookDir;
        spawner = _Spawner;
    }
}

