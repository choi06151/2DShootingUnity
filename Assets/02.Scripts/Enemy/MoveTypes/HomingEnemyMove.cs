using System;
using Unity.VisualScripting;
using UnityEngine;

public class HomingEnemyMove : EnemyMove
{
    private Vector3 _direction;


    protected override void Move()
    {
        _direction = (_player.transform.position - transform.position).normalized;
        MovementCommand movementCommand = new MovementCommand(this.gameObject,
            _direction * Time.deltaTime * _moveSpeed);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }
}