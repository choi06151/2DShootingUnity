using System;
using Unity.VisualScripting;
using UnityEngine;

public class ToPlayerDirectionEnemyMove : EnemyMove
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