using System;
using Unity.VisualScripting;
using UnityEngine;

public class HomingEnemy : Enemy
{
    private Vector3 _direction;
    private PlayerMove _playerMove;

    private void Start()
    {
        _playerMove = CommandManager.Instance.PlayerMove;
    }

    protected override void Move()
    {
        _direction = (_playerMove.transform.position - transform.position).normalized;
        MovementCommand movementCommand = new MovementCommand(this.gameObject,
            _direction * Time.deltaTime * MoveSpeed);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }
}