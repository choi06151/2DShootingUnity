using UnityEngine;

[RequireComponent(typeof(EnemyMove), typeof(EnemyInfo))]
public class Enemy : MonoBehaviour
{
    private EnemyMove _enemyMove;
    private EnemyInfo _enemyInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyInfo = GetComponent<EnemyInfo>();
        _enemyMove = GetComponent<EnemyMove>();
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