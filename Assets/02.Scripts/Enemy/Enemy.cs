using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float MoveSpeed = 3f;

    private float _hp = 100;

    public float Hp
    {
        get => _hp;
        set => _hp = value;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected abstract void Move();


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
        }
        else if (other.tag == "PlayerBullet")
        {
            BulletMove bulletMove = other.GetComponent<BulletMove>();
            Destroy(other.gameObject);
            GetDamage(bulletMove.Damage);
        }
    }

    private void GetDamage(float damage)
    {
        _hp -= damage;
        if (_hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}