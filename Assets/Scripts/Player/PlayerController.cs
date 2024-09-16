using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

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

    [Header("Scale")]
    public int PlayerLevel = 10;
   
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public CharacterStats PlayerStats => _playerStats;
    [SerializeField] private CharacterStats _playerStats;

    public void UpdateScaleText(int amount)
    {
        PlayerLevel -= amount;
        float scale = PlayerLevel *  0.1f;
        transform.localScale = new Vector3(scale, scale, scale);
        _tmproText.text = PlayerLevel.ToString();
        if(_cam.m_Lens.OrthographicSize>2.5f && _cam.m_Lens.OrthographicSize<12.5)
        _cam.m_Lens.OrthographicSize -= amount*_cineMachine;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.Enemy_Tag)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if(enemy.Level<= PlayerLevel)
            {
                UpdateScaleText(-enemy.Level);
               enemy.ReturnToPool();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.Bait_Tag)
        {
            UpdateScaleText(-1);
            BaitPool.ReturnObject(collision.gameObject);
        }
    }

}
