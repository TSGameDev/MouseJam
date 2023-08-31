using UnityEngine;

public class GravityWell : MonoBehaviour, IObjectPoolItem
{
    [SerializeField] private float pullRadius;
    [SerializeField] private float pullForceIncriment;
    [SerializeField] private float duration;
    [SerializeField] private LayerMask enemyLayer;

    private float _CurrentTime = 0f;

    public GameObject GetGameObject() => gameObject;

    private void Update()
    {
        DurationTimer();
    }

    private void FixedUpdate()
    {
        GravityWellFunctionality();
    }

    private void DurationTimer()
    {
        if (_CurrentTime >= duration)
            gameObject.SetActive(false);
        else
            _CurrentTime += 1 * Time.deltaTime;
    }

    private void GravityWellFunctionality()
    {
        Collider2D[] _enemiesInRange = Physics2D.OverlapCircleAll(transform.position, pullRadius, enemyLayer);
        foreach (Collider2D collider in _enemiesInRange)
        {
            Transform _EnemyTrans = collider.gameObject.transform;
            Vector3 _EnemyDirTrans = (transform.position - _EnemyTrans.position).normalized;
            collider.gameObject.transform.position += _EnemyDirTrans * pullForceIncriment * Time.deltaTime;
        }
    }

    public void Reset(ObjectPoolItemData _NextItemResetData)
    {
        transform.position = _NextItemResetData.spawnPosition;
        _CurrentTime = 0f;
        gameObject.SetActive(true);
    }

    public bool IsActive() => gameObject.activeSelf;
}
