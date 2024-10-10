using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : Weapon
{
    [SerializeField] private float _orbitRadius;
    void Start()
    {
        
    }

    void Update()
    {
        OrbiterMovement();
    }
    void OrbiterMovement()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        float angle = Time.time * WeaponState.Speed;

        float x = Mathf.Cos(angle) * _orbitRadius;
        float y = Mathf.Sin(angle) * _orbitRadius;

        Vector3 orbitPos = new Vector3(x, y, 0) + playerPos;

        PlayerController.Instance.OrbitPos.position = orbitPos;
    }
}
