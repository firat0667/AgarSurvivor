using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifeTime = 2f;       
    private ObjectPool objectPool;
    private Vector2 direction;         

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), _lifeTime);
    }

    private void Update()
    {
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    public void SetObjectPool(ObjectPool pool)
    {
        objectPool = pool;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    public void ReturnToPool()
    {
        objectPool.ReturnObject(gameObject);
    }

    private void OnDisable() { CancelInvoke();}
}
