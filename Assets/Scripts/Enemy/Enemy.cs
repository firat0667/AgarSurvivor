using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private float _minScale = 1f;
    [SerializeField] private float _maxScale = 10f;
    [SerializeField] private float _scaleMultiplier = 0.1f;
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private TextMeshPro _levelText;

    public float CharSpeed;

    private Transform _playerTransform;
    private PlayerMovement _playerMovement;
    private float _originalMoveSpeed;
    private bool _isFollowPlayer;

    public int Level { get; set; }

    private void OnEnable()
    {
        _playerTransform = PlayerController.Instance.transform;
        _playerMovement = PlayerController.Instance.PlayerMovement;

        float randomSpeedMultiplier = Random.Range(1, _characterStats.MoveSpeed);
        _originalMoveSpeed = randomSpeedMultiplier;
        CharSpeed = randomSpeedMultiplier;

        SetRandomScale();
    }

    private void Update()
    {
        if (_playerTransform != null && _isFollowPlayer)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 playerDirection = _playerMovement.GetMoveDirection();
        if (playerDirection == Vector2.zero)
        {
            CharSpeed = _originalMoveSpeed;
            return;
        }

        Vector2 enemyToPlayerDirection = (_playerTransform.position - transform.position).normalized;

        // Dot product hesaplaması
        float dotProduct = Vector2.Dot(playerDirection, enemyToPlayerDirection);

        Debug.Log("Dot Product: " + dotProduct);

        // Eğer dot product yüksekse (yani, oyuncu düşmana doğru bakıyorsa), hız azaltılacak
        if (dotProduct > 0.9f)
        {
            CharSpeed = _originalMoveSpeed;
            Debug.Log("Player is facing the enemy, slowing down to 1");
        }
        else
        {
            
            CharSpeed = 1f; // Hızı azalt
            Debug.Log("Player is not facing the enemy, restoring original speed: " + _originalMoveSpeed);
        }
            transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, CharSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag==Tags.Bullet_Tag)
        {
            TakeDamage(2);
            collision.gameObject.GetComponent<Bullet>().ReturnToPool();
            PlayerController.Instance.BaitSpawner.SpawnBait(transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.Dedector_Tag)
            _isFollowPlayer = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Tags.Dedector_Tag)
            _isFollowPlayer = false;
    }

    public void ReturnToPool()
    {
        if (_objectPool != null)
        {
            _objectPool.ReturnObject(gameObject);
        }
    }

    public void SetObjectPool(ObjectPool pool)
    {
        _objectPool = pool;
    }

    private void SetRandomScale()
    {
        float randomScaleMultiplier = Random.Range(_minScale, _maxScale);
        float finalScale = (PlayerController.Instance.PlayerLevel*0.1f) + (randomScaleMultiplier * _scaleMultiplier);
        Vector3 newScale = new Vector3(finalScale, finalScale, finalScale);
        transform.localScale = newScale;

        Level = Mathf.RoundToInt(finalScale * 10);
        _levelText.text = Level.ToString();
    }
    public void TakeDamage(int DamagePower)
    {
        Level -= DamagePower;
        _levelText.text = Level.ToString();
        float finalScale = Level * 0.1f;
        Vector3 newScale = new Vector3(finalScale, finalScale, finalScale);
        transform.localScale = newScale;
        if (Level <= 0)
        {
            ReturnToPool();

        }
    }
}
