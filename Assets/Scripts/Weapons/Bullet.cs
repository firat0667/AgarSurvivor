using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    private ObjectPool _objectPool;
    private Vector2 direction;

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), WeaponState.Range);
    }
    private void Update()
    {
        transform.Translate(direction * WeaponState.Speed * Time.deltaTime);
    }
    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }
    public void SetObjectPool(ObjectPool pool)
    {
        _objectPool = pool;
    }

    public void ReturnToPool()
    {
        _objectPool.ReturnObject(gameObject);
    }

    private void OnDisable() { CancelInvoke(); }
}
