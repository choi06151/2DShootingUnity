using UnityEngine;

[RequireComponent(typeof(EnemyFunMove), typeof(EnemyFunInfo))]
public class Enemy : MonoBehaviour
{
    private EnemyFunMove _enemyFunMove;
    private EnemyFunInfo _enemyFunInfo;


    [Header("적 기본 속도")] [SerializeField] protected float _enemySpeed;
    [Header("적 기본 체력")] [SerializeField] protected float _enemyHP;
    [Header("적 기본 데미지")] [SerializeField] protected float _enemyDamage;

    public float EnemySpeed => _enemySpeed;
    public float EnemyHp => _enemyHP;
    public float EnemyDamage => _enemyDamage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyFunInfo = GetComponent<EnemyFunInfo>();
        _enemyFunMove = GetComponent<EnemyFunMove>();

        _enemyFunInfo.Init(this);
        _enemyFunMove.Init(this);
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
            player.TakeDamage(_enemyFunInfo.GetDamageInfo());
            _enemyFunInfo.Death();
            Debug.Log("충돌");
        }
        else if (other.tag == "PlayerBullet")
        {
            BulletMove bulletMove = other.GetComponent<BulletMove>();
            Destroy(other.gameObject);
            _enemyFunInfo.GetDamage(bulletMove.Damage);
        }
    }
}