using System;
using UnityEngine;

public class EnemyInfo : MonoBehaviour, IHP, IEnemyFun
{
    public float Hp { get; private set; }

    private Enemy _enemy;

    [Header("죽을때 나올 이펙트 프리팹")]
    [SerializeField] private GameObject _explosionPrefab;

    [Header("죽을때 나올 소리")]
    [SerializeField] private AudioClip _deathSound;


    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        Hp = _enemy.EnemyHp;
    }

    public void GetDamage(float damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            Death();
        }
    }

    public void GetHp(float hp)
    {
        Hp += hp;
        if (Hp > _enemy.EnemyHp)
            Hp = _enemy.EnemyHp;
    }

    public bool IsDeathAbleDamage(float damage)
    {
        if (Hp - damage <= 0)
        {
            return true;
        }

        return false;
    }

    public void Death()
    {
        ScoreManager.Instance.Addscore(10);
        _enemy.EnhanceEnemy();
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        ItemCreator.Instance.CreateItem(this.transform.position);
        AudioManager.Instance.ActivateEffectAudioClip(_deathSound);

        //Destroy(gameObject);
        PoolManager.Instance.InputToPool(gameObject);
    }
}