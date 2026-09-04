using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyMove : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 3f;

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