using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<ZoneSpawner> zoneSpawners = new();
    [SerializeField] private List<GameObject> groundEnemyList = new();
    [SerializeField] private List<GameObject> flyingEnemyList = new();
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

        foreach (ZoneSpawner zone in zoneSpawners)
        {
            if (zone.IsInZone)
            {
                zone.SpawnEnemy(AssignedNewEnemy(zone).name);
                return;
            }
        }
    }

    private GameObject AssignedNewEnemy(ZoneSpawner zone)
    {
        int _RandomEnemy;
        GameObject _NewEnemy = null;
        if (zone.GetAllowGroundEnemies())
        {
            int _GroundOrFly = Random.Range(1, 3);
            switch (_GroundOrFly)
            {
                case 1:
                    _RandomEnemy = Random.Range(1, groundEnemyList.Count);
                    _NewEnemy = groundEnemyList[_RandomEnemy];
                    break;
                case 2:
                    _RandomEnemy = Random.Range(1, flyingEnemyList.Count);
                    _NewEnemy = flyingEnemyList[_RandomEnemy];
                    break;
            }
        }
        else
        {
            _RandomEnemy = Random.Range(1, flyingEnemyList.Count);
            _NewEnemy = flyingEnemyList[_RandomEnemy];
        }
        return _NewEnemy;
    }

    private void SetupObjectPool()
    {
        foreach (GameObject enemy in groundEnemyList)
        {
            ObjectPool.CreateObjectPool(enemy.name, enemy.GetComponent<IObjectPoolItem>(), maxNumberOfEnemies);
        }

        foreach (GameObject enemy in flyingEnemyList)
        {
            ObjectPool.CreateObjectPool(enemy.name, enemy.GetComponent<IObjectPoolItem>(), maxNumberOfEnemies);
        }
    }
}
