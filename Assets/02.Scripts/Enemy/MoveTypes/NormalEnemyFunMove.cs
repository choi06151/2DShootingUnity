using System;
using Unity.VisualScripting;
using UnityEngine;

public class NormalEnemyFunMove : EnemyFunMove
{
    private Vector3 _direction = new Vector3(0, -1, 0);

    protected override void Move()
    {
        MovementCommand movementCommand = new MovementCommand(this.gameObject,
            _direction * Time.deltaTime * _moveSpeed);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }
}