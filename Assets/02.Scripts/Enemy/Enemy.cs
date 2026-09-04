using UnityEngine;

[RequireComponent(typeof(EnemyMove), typeof(EnemyInfo))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyCreator _enemyCreator;
    private EnemyMove _enemyMove;
    private EnemyInfo _enemyInfo;


    [Header("적 기본 속도")] [SerializeField] protected float _enemySpeed;
    [Header("적 기본 체력")] [SerializeField] protected float _enemyHP;
    [Header("적 기본 데미지")] [SerializeField] protected float _enemyDamage;

    public float EnemySpeed => _enemySpeed;
    public float EnemyHp => _enemyHP;
    public float EnemyDamage => _enemyDamage;

    public EnemyCreator GetEnemyCreator => _enemyCreator;


    public void InitEnemy(EnemyCreator enemyCreator)
    {
        _enemyCreator = enemyCreator;

        _enemyInfo = GetComponent<EnemyInfo>();
        _enemyMove = GetComponent<EnemyMove>();

        _enemyInfo.Init(this);
        _enemyMove.Init(this);
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
            Debug.Log("충돌");
        }
        else if (other.tag == "PlayerBullet")
        {
            BulletMove bulletMove = other.GetComponent<BulletMove>();
            Destroy(other.gameObject);
            _enemyInfo.GetDamage(bulletMove.Damage);
        }
    }
}