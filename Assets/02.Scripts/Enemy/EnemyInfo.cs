using System;
using UnityEngine;

public class EnemyInfo : MonoBehaviour, IHP, IEnemyFun
{
    public float Hp { get; private set; }
    public float MaxHp { get; private set; }

    private float _damage;
    private GameObject _exlosionPrefab;

    public void Init(Enemy enemy)
    {
        MaxHp = enemy.EnemyHp;
        Hp = MaxHp;
        _damage = enemy.EnemyDamage;
        _exlosionPrefab = enemy.ExplosionPrefab;
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
        Instantiate(_exlosionPrefab, transform.position, Quaternion.identity);
        ItemCreator.Instance.CreateItem(this.transform.position);
        //Destroy(gameObject);
        PoolManager.Instance.InputToPool(gameObject);
    }
}