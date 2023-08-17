using System;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPool
{
    private static Dictionary<string, Queue<IObjectPoolItem>> _ObjectPoolMain = new();
    private static Transform _ObjectPoolsParent;

    private static bool CheckObjectPool(string _ObjectPoolKey) => _ObjectPoolMain.ContainsKey(_ObjectPoolKey);

    public static Queue<IObjectPoolItem> CreateObjectPool(string _ObjectPoolKey, IObjectPoolItem _Object, int _PoolAmount)
    {
        if (!CheckObjectPool(_ObjectPoolKey))
        {
            if (_ObjectPoolsParent == null)
                _ObjectPoolsParent = new GameObject("Object Pools").transform;

            Transform _ItemParent = new GameObject(_ObjectPoolKey).transform;
            _ItemParent.parent = _ObjectPoolsParent.transform;

            Queue<IObjectPoolItem> _NewObjectPoolQueue = new();
            for(int i = 0; i < _PoolAmount; i++)
            {
                GameObject _NewGameObject = GameObject.Instantiate(_Object.GetGameObject(), _ItemParent);
                _NewGameObject.SetActive(false);

                IObjectPoolItem _NewObjectpoolItem = _NewGameObject.GetComponent<IObjectPoolItem>();
                _NewObjectPoolQueue.Enqueue(_NewObjectpoolItem);
            }
            _ObjectPoolMain.Add(_ObjectPoolKey, _NewObjectPoolQueue);
            return _NewObjectPoolQueue;
        }
        else
        {
            throw new Exception("Object Pool with this key already exists.");
        }
    }

    public static void SpawnItem(string _ObjectPoolKey, ObjectPoolItemData _NextItemResetData)
    {
        Queue<IObjectPoolItem> _ItemObjectPool;
        if (CheckObjectPool(_ObjectPoolKey))
        {
            bool _GotObjectPool = _ObjectPoolMain.TryGetValue(_ObjectPoolKey, out _ItemObjectPool);
            if(!_GotObjectPool)
                throw new Exception("Couldn't collect Object Pool");

            IObjectPoolItem _NextItem = _ItemObjectPool.Dequeue();
            _NextItem.Reset(_NextItemResetData);
            _ItemObjectPool.Enqueue(_NextItem);
        } 
        else
        {
            throw new Exception("Object Pool with this key doesn't exists, please create one first.");
        }
    }
}
