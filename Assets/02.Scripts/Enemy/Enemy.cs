using UnityEngine;

[RequireComponent(typeof(EnemyMove), typeof(EnemyInfo))]
public class Enemy : PoolingObject
{
    [SerializeField] private EnemyCreator _enemyCreator;
    private EnemyMove _enemyMove;
    private EnemyInfo _enemyInfo;
    private EnemyAnimationControl _enemyAnimationControl;

    [Header("적 기본 속도")] [SerializeField] protected float _enemySpeed;
    [Header("적 기본 체력")] [SerializeField] protected float _enemyHP;
    [Header("적 기본 데미지")] [SerializeField] protected float _enemyDamage;

    [Header("죽을때 나올 이펙트 프리팹")] [SerializeField]
    private GameObject _explosionPrefab;

    [Header("죽을때 나올 소리")] [SerializeField] private AudioClip _deathSound;

    private float _originalSpeed;


    public float EnemySpeed => _enemySpeed;
    public float EnemyHp => _enemyHP;
    public float EnemyDamage => _enemyDamage;

    public GameObject ExplosionPrefab => _explosionPrefab;


    public EnemyCreator GetEnemyCreator => _enemyCreator;
    public AudioClip DeathSound => _deathSound;

    public void InitEnemy(EnemyCreator enemyCreator)
    {
        _enemyCreator = enemyCreator;

        _enemyInfo = GetComponent<EnemyInfo>();
        _enemyMove = GetComponent<EnemyMove>();
        _enemyAnimationControl = GetComponent<EnemyAnimationControl>();

        _enemyInfo.Init(this);
        _enemyMove.Init(this);
        _enemyAnimationControl.Init(this);

        _originalSpeed = _enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.TakeDamage(_enemyInfo.GetDamageInfo());
            _enemyInfo.Death();
        }
    }

    public bool CheckDeathDamage(float damage)
    {
        if (_enemyInfo.IsDeathAbleDamage(damage))
        {
            return true;
        }

        return false;
    }

    public void TakeDamage(float damage)
    {
        _enemyInfo.GetDamage(damage);
        _enemyAnimationControl.TriggerDamageAnimation();
    }

    public void TakeSlowEffect(float amount)
    {
        _enemySpeed = _enemySpeed - _enemySpeed * amount;
        _enemyMove.Init(this);
    }

    public void ClearSlowEffect()
    {
        _enemySpeed = _originalSpeed;
        _enemyMove.Init(this);
    }

    public void EnhanceEnemy()
    {
        _enemyHP = _enemyHP * 1.6f;
        Debug.Log($"강화됌{_enemyHP}");
    }
}