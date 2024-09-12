using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
   [SerializeField] private ObjectPool _objectPool;    
   [SerializeField] PlayerMovement _playerMove;
   [SerializeField] private int _scaleLosePerBullet = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PlayerController.Instance.PlayerScale>=2) 
        {
            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = _objectPool.GetObject();
        bullet.transform.position = transform.position;  
        bullet.transform.rotation = transform.rotation;
        Vector3 scale = PlayerController.Instance.transform.localScale;
        bullet.transform.localScale = scale;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetObjectPool(_objectPool); 


        Vector2 moveDirection = _playerMove.GetMoveDirection();  
        bulletScript.SetDirection(moveDirection);
        PlayerController.Instance.UpdateScaleText(_scaleLosePerBullet);
    }
}
