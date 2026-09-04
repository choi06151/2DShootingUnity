using System;
using Unity.VisualScripting;
using UnityEngine;

public class ToPlayerDirectionEnemyFunMove : EnemyFunMove
{
    private Vector3 _direction;

    private void Start()
    {
        _direction = (CommandManager.Instance.PlayerMove.transform.position - transform.position).normalized;
    }

    protected override void Move()
    {
        MovementCommand movementCommand =
            new MovementCommand(this.gameObject, _direction * Time.deltaTime * _moveSpeed);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }
}