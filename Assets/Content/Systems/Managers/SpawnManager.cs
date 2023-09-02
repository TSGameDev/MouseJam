using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<ZoneSpawner> zoneSpawners = new();
    [SerializeField] private List<GameObject> enemyList = new();
    [SerializeField] private int maxNumberOfEnemies = 10;

    private float _SpawnTimer = 5f;
    private float _PhaseTimer = 30f;
    private float _CurrentSpawnTime = 0f;
    private float _CurrentPhaseTimer = 0f;

    private void Start()
    {
        SetupObjectPool();
    }

    private void Update()
    {
        Countdownimers();
    }

    private void Countdownimers()
    {
        if(_CurrentSpawnTime >= _SpawnTimer)
        {
            SpawnNewEnemy();
            _CurrentSpawnTime = 0;
        }else
        {
            _CurrentSpawnTime += 1 * Time.deltaTime;
        }

        if (_CurrentPhaseTimer >= _PhaseTimer && _SpawnTimer > 0.5f)
        {
            _CurrentPhaseTimer = 0;
            _SpawnTimer -= 0.5f;
        }else
        {
            _CurrentPhaseTimer += 1 * Time.deltaTime;
        }
    }

    private void SpawnNewEnemy()
    {
        if (zoneSpawners.Count <= 0)
            return;

        int _RandomNum = Random.Range(0, enemyList.Count);
        GameObject _NewEnemy = enemyList[_RandomNum];

        foreach(ZoneSpawner zone in zoneSpawners)
        {
            if (zone.IsInZone)
            {
                zone.SpawnEnemy(_NewEnemy.name);
            }
        }
    }

    private void SetupObjectPool()
    {
        foreach (GameObject enemy in enemyList)
        {
            ObjectPool.CreateObjectPool(enemy.name, enemy.GetComponent<IObjectPoolItem>(), maxNumberOfEnemies);
        }
    }
}
