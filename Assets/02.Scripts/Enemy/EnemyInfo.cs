using System;
using UnityEngine;

public class EnemyInfo : MonoBehaviour, IHP, IEnemyFun
{
    public float Hp { get; private set; }
    public float MaxHp { get; private set; }

    private float _damage;
    private GameObject _exlosionPrefab;
    private AudioClip _deathSound;
    private Enemy _enemy;

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        MaxHp = enemy.EnemyHp;
        Hp = MaxHp;
        _damage = enemy.EnemyDamage;
        _exlosionPrefab = enemy.ExplosionPrefab;
        _deathSound = enemy.DeathSound;
    }


    public float GetDamageInfo()
    {
        return _damage;
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
        if (Hp > MaxHp)
            Hp = MaxHp;
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
        Instantiate(_exlosionPrefab, transform.position, Quaternion.identity);
        ItemCreator.Instance.CreateItem(this.transform.position);
        AudioManager.Instance.ActivateEffectAudioClip(_deathSound);

        //Destroy(gameObject);
        PoolManager.Instance.InputToPool(gameObject);
    }
}