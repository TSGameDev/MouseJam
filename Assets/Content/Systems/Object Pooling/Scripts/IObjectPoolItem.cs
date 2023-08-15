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
    public Vector3 characterLookDir;

    public ObjectPoolItemData(Vector3 _SpawnPosition = new(), Vector3 _CharacterLookDir = new())
    {
        spawnPosition = _SpawnPosition;
        characterLookDir = _CharacterLookDir;
    }
}

