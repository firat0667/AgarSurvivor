using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private ObjectPool _enemyPool; 
    [SerializeField] private float _starSpawnInterval = 5f;  
    [SerializeField] private Transform _spawnArea;      

    private Vector2 _spawnAreaMin;  
    private Vector2 _spawnAreaMax;  



    private void Start()
    {
        
        SetSpawnAreaBounds();

        InvokeRepeating(nameof(SpawnEnemy), 0f, _starSpawnInterval/PlayerController.Instance.PlayerLevel);
    }

    private void SetSpawnAreaBounds()
    {
        Bounds bounds = _spawnArea.GetComponent<Collider2D>().bounds;
        _spawnAreaMin = bounds.min;
        _spawnAreaMax = bounds.max;
    }

    private void SpawnEnemy()
    {
        GameObject enemy = _enemyPool.GetObject();

        if (enemy == null)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        float randomX = Random.Range(_spawnAreaMin.x, _spawnAreaMax.x);
        float randomY = Random.Range(_spawnAreaMin.y, _spawnAreaMax.y);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        enemy.transform.position = randomPosition;

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.SetObjectPool(_enemyPool);
        }
    }
}
