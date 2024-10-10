using TMPro;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Square
}

public class Enemy : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private int _minScale = 1;
    [SerializeField] private int _maxScale = 10;
    [SerializeField] private float _scaleMultiplier = 0.1f;
    [SerializeField] private CharacterStats _characterStats;
    [SerializeField] private TextMeshPro _levelText;

    private float _charSpeed;

    private Transform _playerTransform;
    private PlayerMovement _playerMovement;
    private float _originalMoveSpeed;
    private bool _isFollowPlayer;

    public EnemyType EnemyType;

    public int Level { get; set; }

    // Bu ayarları Inspector'da koşullu göstermek için Custom Editor yazacağız
     public float Frequency { get; set;}
     public float Amplitude { get; set;}

    private void OnEnable()
    {
        _playerTransform = PlayerController.Instance.transform;
        _playerMovement = PlayerController.Instance.PlayerMovement;

        float randomSpeedMultiplier = Random.Range(1, _characterStats.MoveSpeed);
        _originalMoveSpeed = randomSpeedMultiplier;
        _charSpeed = randomSpeedMultiplier;

        SetRandomScale();
    }

    private void Update()
    {
        if (_playerTransform != null && _isFollowPlayer && EnemyType == EnemyType.Normal && PlayerController.Instance.PlayerLevel<Level)
        {
            MoveTowardsPlayer();
        }
        else if (_playerTransform != null && _isFollowPlayer && PlayerController.Instance.PlayerLevel >= Level)
        {
            FleeFromPlayer();
        }
        else if (EnemyType == EnemyType.Square)
        {
            transform.position += WaveMove(PlayerController.Instance.transform.position) * Time.deltaTime;
        }
    }

    private void MoveTowardsPlayer()
    {
        if (EnemyType == EnemyType.Normal)
        {
            Vector2 playerDirection = _playerMovement.GetMoveDirection();
            if (playerDirection == Vector2.zero)
            {
                _charSpeed = _originalMoveSpeed;
                return;
            }

            Vector2 enemyToPlayerDirection = (_playerTransform.position - transform.position).normalized;

            float dotProduct = Vector2.Dot(playerDirection, enemyToPlayerDirection);

            Debug.Log("Dot Product: " + dotProduct);

            if (dotProduct > 0.9f)
            {
                _charSpeed = _originalMoveSpeed;
                Debug.Log("Player is facing the enemy, slowing down to 1");
            }
            else
            {
                _charSpeed = 1f;
                Debug.Log("Player is not facing the enemy, restoring original speed: " + _originalMoveSpeed);
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, _charSpeed * Time.deltaTime);
    }

    public Vector3 WaveMove(Vector3 playerPos)
    {
        if (EnemyType != EnemyType.Square)
            return Vector3.zero;

        Vector3 returnVelocity = playerPos - transform.position;

        Vector3 parallel = Vector3.Cross(returnVelocity, Vector3.forward);

        returnVelocity += parallel * Mathf.Sin(Time.time * Frequency) * Amplitude;

        return returnVelocity.normalized * _characterStats.MoveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.Weapon_Tag)
        {
            Weapon weapon = collision.gameObject.GetComponent<Weapon>();
            TakeDamage(weapon.WeaponState.Damage);
            if (weapon.WeaponState == UpgradeManager.Instance.BulletState)
            {
                weapon.GetComponent<Bullet>().ReturnToPool();
            }
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
        Level = PlayerController.Instance.PlayerLevel * Random.Range(_minScale, _maxScale);
        float finalScale = 1 + (Level * 0.1f);
        Vector3 newScale = new Vector3(finalScale, finalScale, finalScale);
        transform.localScale = newScale;

        _levelText.text = Level.ToString();
    }
    private void FleeFromPlayer()
    {
        Vector3 directionAwayFromPlayer = (transform.position - _playerTransform.position).normalized;
        transform.position += directionAwayFromPlayer * _charSpeed * Time.deltaTime;
    }
    public void TakeDamage(int DamagePower)
    {
        Level -= DamagePower;
        float finalScale = 1 + (Level * 0.1f);
        _levelText.text = Level.ToString();

        Vector3 newScale = new Vector3(finalScale, finalScale, finalScale);
        transform.localScale = newScale;
        if (Level <= 0)
        {
            ReturnToPool();
            ParticleManager.Instance.CreateParticle(transform, EnemyType);
        }
    }
}
