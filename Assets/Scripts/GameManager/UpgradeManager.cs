using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum WeaponType
{
  Bullet,
  Orbit,
  Bomb
}



public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;
    [Header("Weapons")]
    public WeaponState BulletState;
    public WeaponState OrbitState;
    public WeaponState BombState;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
