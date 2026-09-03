using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 3f;
    private Vector3 _direction = new Vector3(0, -1, 0);

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

    private void Move()
    {
        MovementCommand movementCommand = new MovementCommand(this.gameObject,
            _direction * Time.deltaTime * MoveSpeed);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
        }
        else if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            GetDamage(100);
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