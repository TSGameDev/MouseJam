using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> zoneSpawners = new List<GameObject>();

    private bool isInZone = false;
    public bool IsInZone { get { return isInZone; } }

    private List<GameObject> _ZoneSpawnedEnemies = new();
    public void RemoveEnemyFromZone(GameObject _Enemy)
    {
        foreach(GameObject enemy in _ZoneSpawnedEnemies)
        {
            if(enemy == _Enemy)
            {
                _ZoneSpawnedEnemies.Remove(enemy);
                return;
            }
        }
    }

    public void SpawnEnemy(string _EnemyKey)
    {
        int _RandomNum = Random.Range(0, zoneSpawners.Count);
        GameObject _Spanwer = zoneSpawners[_RandomNum];
        GameObject _NewSpawnEnemy = ObjectPool.SpawnItem(_EnemyKey, new ObjectPoolItemData(_Spanwer.transform.position, new(), gameObject));
        _ZoneSpawnedEnemies.Add(_NewSpawnEnemy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            isInZone = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            isInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(GameObject enemy in _ZoneSpawnedEnemies)
                enemy.SetActive(false);

            _ZoneSpawnedEnemies.Clear();

            isInZone = false;
        }
    }
}
