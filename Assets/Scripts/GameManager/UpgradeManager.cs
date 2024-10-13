using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Bullet,
    Orbit,
    Sword
}

public enum UpgradeType
{
    MoveSpeed,
    ExpAmount,
    OrbitDamage,
    OrbitAmount,
    OrbitSpeed,
    BulletDamage,
    BulletFrequency
}

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("Weapons")]
    public WeaponState BulletState;
    public WeaponState OrbitState;
    public WeaponState SwordState;

    public GameObject OrbitPrefab;

    public GameObject UpgradePanel;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start() { }

    void Update() { }

    public void IncreaseProperty(UpgradeType upgradeType, int amount) // amount parametre olarak alınıyor
    {
        var playerStats = PlayerController.Instance.PlayerStats;

        switch (upgradeType)
        {
            case UpgradeType.MoveSpeed:
                playerStats.MoveSpeed += amount; 
                break;

            case UpgradeType.ExpAmount:
                playerStats.Exp += amount; 
                break;

            case UpgradeType.OrbitDamage:
                OrbitState.Damage += amount; 
                break;

            case UpgradeType.OrbitAmount:
                Instantiate(OrbitPrefab);
                break;

            case UpgradeType.OrbitSpeed:
                OrbitState.Speed += amount; 
                break;

            case UpgradeType.BulletDamage:
                BulletState.Damage += amount; 
                break;

            case UpgradeType.BulletFrequency:
                BulletState.Frequency += amount; 
                break;

            default:
                Debug.LogWarning("Geçersiz UpgradeType");
                break;
        }
    }
}
