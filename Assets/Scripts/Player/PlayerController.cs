using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public PlayerMovement PlayerMovement => _playerMovement;
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private TextMeshPro _tmproText;
    [SerializeField] private CinemachineVirtualCamera _cam;
    [SerializeField] private float _cineMachine = 0.5f;

    [Header("Scale")]
    public int PlayerScale = 10;
   
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
        PlayerScale -= amount;
        float scale = PlayerScale *  0.1f;
        transform.localScale = new Vector3(scale, scale, scale);
        _tmproText.text = PlayerScale.ToString();
        _cam.m_Lens.OrthographicSize -= _cineMachine;

    }
}
