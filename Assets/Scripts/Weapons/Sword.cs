using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        SwordMovement();
    }
    void SwordMovement()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        float angle = Time.time * WeaponState.Speed;

        float x = Mathf.Cos(angle);
        float y = Mathf.Sin(angle);

        Vector3 orbitPos = new Vector3(x, y, 0) + playerPos;

        PlayerController.Instance.OrbitPos.position = orbitPos;
    }
}
