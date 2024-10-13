using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public PlayerMovement PlayerMovement => _playerMovement;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private float _cineMachine = 0.5f;



    public BaitSpawner BaitSpawner;
    public ObjectPool BaitPool;

    public int AttackPower;

    [Header("Scale")]
    public int PlayerLevel = 1;

    public CharacterStats PlayerStats => _playerStats;
    [SerializeField] private CharacterStats _playerStats;


    [Header("Weapons")]

    public Transform OrbitPos;
    public List<Transform> Orbiters = new List<Transform>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        PlayerStats.AttackPower = 1;
        AttackPower = PlayerStats.AttackPower;
    }
    private void Start()
    {

    }

  

    public void UpdateScaleText()
    {
        float scale = 1+ (PlayerLevel * 0.1f);
        PlayerLevel += 1;
        Time.timeScale = 0;
        UpgradeManager.Instance.UpgradePanel.SetActive(true);
        transform.localScale = new Vector3(scale, scale, scale);
        if (PlayerLevel <= 0)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.Enemy_Tag)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();


            if (enemy.Level <= PlayerLevel)
            {
                ExperienceScript.Instance.EarnExp(enemy.Level*_playerStats.Exp);
                ParticleManager.Instance.CreateParticle(transform, enemy.EnemyType);
            }
            else
            {
                GameManager.Instance.DeadScore();
                Scene currentScene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(currentScene.name);
            }
         
            enemy.ReturnToPool();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.Bait_Tag)
        {
            ExperienceScript.Instance.EarnExp(_playerStats.Exp);
            BaitPool.ReturnObject(collision.gameObject);
        }
    }

}
