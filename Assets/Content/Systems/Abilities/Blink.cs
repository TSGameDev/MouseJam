using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour, IObjectPoolItem
{
    [SerializeField] private float durationOfEffect;
    private float _CurrentDuration = 0;

    public GameObject GetGameObject() => gameObject;

    private void Update()
    {
        if (_CurrentDuration < durationOfEffect)
            _CurrentDuration += 1 * Time.deltaTime;
        else
            gameObject.SetActive(false);
    }

    public void Reset(ObjectPoolItemData _NextItemResetData)
    {
        _CurrentDuration = 0;
        transform.position = _NextItemResetData.spawnPosition;
        gameObject.SetActive(true);
    }

    public bool IsActive() => gameObject.activeSelf;
}
