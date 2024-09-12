using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    private CharacterStats _playerstats;

    private Vector2 moveDirection = Vector2.right;

    private void Awake()
    {
        _playerstats = _playerTransform.GetComponent<PlayerController>().PlayerStats;
    }

    void Update()
    {
        Move();
        UpdateMoveDirection();  
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0f);
        _playerTransform.Translate(movement * _playerstats.MoveSpeed * Time.deltaTime);
    }

    private void UpdateMoveDirection()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (moveX != 0 || moveY != 0)
        {
            moveDirection = new Vector2(moveX, moveY).normalized;  
        }
    }

    public Vector2 GetMoveDirection()
    {
        return moveDirection;
    }
}
