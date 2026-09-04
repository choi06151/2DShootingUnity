using System;
using UnityEngine;

public class EnemyFunInfo : MonoBehaviour, IHP, IEnemyFun
{
    public float Hp { get; private set; }
    public float MaxHp { get; private set; }

    private float _damage;

    public void Init(Enemy enemy)
    {
        MaxHp = enemy.EnemyHp;
        Hp = MaxHp;
        _damage = enemy.EnemyDamage;
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

    public void Death()
    {
        Destroy(gameObject);
    }
}