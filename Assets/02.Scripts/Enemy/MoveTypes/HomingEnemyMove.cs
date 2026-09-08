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


        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
}