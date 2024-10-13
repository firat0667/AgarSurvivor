using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : Weapon
{
    [SerializeField] private float _orbitRadius;
    private void OnEnable()
    {
        if(!PlayerController.Instance.Orbiters.Contains(transform))
        PlayerController.Instance.Orbiters.Add(transform);
    }

    void Update()
    {
        OrbiterMovement();
    }
    void OrbiterMovement()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;
        float angleStep = 360f / PlayerController.Instance.Orbiters.Count; 

        for (int i = 0; i < PlayerController.Instance.Orbiters.Count; i++)
        {
            float angle = (Time.time * WeaponState.Speed + angleStep * i) * Mathf.Deg2Rad; // Convert angle to radians and apply step
            float x = Mathf.Cos(angle) * _orbitRadius; // Calculate x position
            float y = Mathf.Sin(angle) * _orbitRadius; // Calculate y position

            Vector3 orbitPos = new Vector3(x, y, 0) + playerPos; // Calculate orbit position
            PlayerController.Instance.Orbiters[i].position = orbitPos; // Set orbiter's position
        }
    }
}
