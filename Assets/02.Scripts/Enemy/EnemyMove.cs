using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyMove : MonoBehaviour, IEnemyFun
{
    protected float _moveSpeed;
    protected Player _player;

    public void Init(Enemy enemy)
    {
        _moveSpeed = enemy.EnemySpeed;
        _player = enemy.GetEnemyCreator.GetPlayer;
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