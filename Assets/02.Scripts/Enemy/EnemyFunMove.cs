using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyFunMove : MonoBehaviour, IEnemyFun
{
    protected float _moveSpeed;

    public void Init(Enemy enemy)
    {
        _moveSpeed = enemy.EnemySpeed;
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
}