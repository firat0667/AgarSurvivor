﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private PlayerMovement _playerMove;
    [SerializeField] private float _scaleLosePerBullet = 0.01f;

    private void Start()
    {
        // Otomatik olarak mermi spawn etmek için Coroutine başlatıyoruz.
        StartCoroutine(SpawnBulletsAutomatically());
    }

    private IEnumerator SpawnBulletsAutomatically()
    {
        while (true)
        {
            if (PlayerController.Instance.PlayerLevel >= 1)
            {
                SpawnBullet();
            }

            yield return new WaitForSeconds(UpgradeManager.Instance.BulletState.Frequency);
        }
    }

    private void SpawnBullet()
    {
        GameObject bullet = _objectPool.GetObject();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;

        // Mermiye oyuncunun ölçeğini veriyoruz.
        Vector3 scale = PlayerController.Instance.transform.localScale;
        bullet.transform.localScale = scale;

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetObjectPool(_objectPool);

        // Oyuncunun hareket yönüne göre mermiyi ateşliyoruz.
        Vector2 moveDirection = _playerMove.GetMoveDirection();
        bulletScript.SetDirection(moveDirection);

        // Oyuncu ölçeğini her mermi ateşlemesinde küçültme (şu an yorum satırında).
        // PlayerController.Instance.UpdateScaleText(_scaleLosePerBullet);
    }
}
