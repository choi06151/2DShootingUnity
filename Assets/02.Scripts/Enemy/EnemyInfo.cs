using System;
using UnityEngine;

public class EnemyInfo : MonoBehaviour, IHP
{
    public float Hp { get; private set; }
    public float MaxHp { get; private set; } = 100;

    [SerializeField] private float _damage;


    private void Start()
    {
        Hp = MaxHp;
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