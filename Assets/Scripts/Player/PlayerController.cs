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

    [SerializeField] private TextMeshPro _tmproText;
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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        PlayerStats.AttackPower = 1;
        AttackPower = PlayerStats.AttackPower;
    }


    public void UpdateScaleText()
    {
        float scale = 1+ (PlayerLevel * 0.1f);
        PlayerLevel += 1;
        PlayerStats.AttackPower += 1;
        AttackPower = PlayerStats.AttackPower;
        transform.localScale = new Vector3(scale, scale, scale);
        //float scale = PlayerLevel *  0.01f;
        //transform.localScale = new Vector3(scale, scale, scale);
        _tmproText.text = PlayerLevel.ToString();
        //if (_cam.m_Lens.OrthographicSize > 2.5f && _cam.m_Lens.OrthographicSize < 12.5)
        //    _cam.m_Lens.OrthographicSize -= amount * _cineMachine;
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
                ExperienceScript.Instance.EarnExp(enemy.Level*5);
            }
            else
            {
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
            ExperienceScript.Instance.EarnExp(5);
            BaitPool.ReturnObject(collision.gameObject);
        }
    }

}
