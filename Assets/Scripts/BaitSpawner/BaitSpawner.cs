using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _baitPool;


    public void SpawnBait(Transform transform)
    {
        GameObject bait = _baitPool.GetObject();

        bait.transform.position = transform.position;
        bait.transform.localScale = transform.localScale*0.25f;

        Bait baitscript = bait.GetComponent<Bait>();
        if (baitscript != null)
        {
            baitscript.SetObjectPool(_baitPool);
        }
     }
    }
