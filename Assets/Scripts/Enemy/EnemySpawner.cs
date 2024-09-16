using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _enemyPool;  // Object Pool referansı
    [SerializeField] private float _spawnInterval = 2f;  // Düşman üretme aralığı
    [SerializeField] private Transform _spawnArea;      // Düşmanların üretileceği alan

    private Vector2 _spawnAreaMin;  // Alanın minimum köşesi
    private Vector2 _spawnAreaMax;  // Alanın maksimum köşesi

    private void Start()
    {
        // Sınırları belirle
        SetSpawnAreaBounds();

        // Düşmanları belirli aralıklarla üret
        InvokeRepeating(nameof(SpawnEnemy), 0f, _spawnInterval);
    }

    private void SetSpawnAreaBounds()
    {
        // SpawnArea'nın sınırlarını belirle
        Bounds bounds = _spawnArea.GetComponent<Collider2D>().bounds;
        _spawnAreaMin = bounds.min;
        _spawnAreaMax = bounds.max;
    }

    private void SpawnEnemy()
    {
        // Object Pool'dan bir düşman al
        GameObject enemy = _enemyPool.GetObject();

        // Eğer havuzda nesne yoksa, düşman üretimini durdur
        if (enemy == null)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        // Sınırlar içinde rastgele bir konum seç
        float randomX = Random.Range(_spawnAreaMin.x, _spawnAreaMax.x);
        float randomY = Random.Range(_spawnAreaMin.y, _spawnAreaMax.y);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        // Düşmanı rastgele konuma yerleştir
        enemy.transform.position = randomPosition;

        // Eğer düşmanın Object Pool referansı varsa, ona ayarla
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.SetObjectPool(_enemyPool);
        }
    }
}
