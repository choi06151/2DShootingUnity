using UnityEngine;

[RequireComponent(typeof(EnemyMove), typeof(EnemyInfo))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyCreator _enemyCreator;
    private EnemyMove _enemyMove;
    private EnemyInfo _enemyInfo;
    private EnemyAnimationControl _enemyAnimationControl;

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
        _enemyAnimationControl = GetComponent<EnemyAnimationControl>();

        _enemyInfo.Init(this);
        _enemyMove.Init(this);
        _enemyAnimationControl.Init(this);
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
        else if (other.tag == "PlayerBullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            Destroy(other.gameObject);
            _enemyInfo.GetDamage(bullet.Damage);
            _enemyAnimationControl.TriggerDamageAnimation();
        }
    }
}