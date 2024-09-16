using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    private ObjectPool _objectPool;
    private void OnEnable()
    {
        _objectPool = PlayerController.Instance.BaitPool;
    }
    public void SetObjectPool(ObjectPool pool)
    {
        _objectPool = pool;
    }
}
