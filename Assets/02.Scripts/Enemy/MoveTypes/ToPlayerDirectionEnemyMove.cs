using System;
using Unity.VisualScripting;
using UnityEngine;

public class ToPlayerDirectionEnemyMove : EnemyMove
{
    private Vector3 _direction;

    private void Start()
    {
        _direction = (_player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

    protected override void Move()
    {
        MovementCommand movementCommand =
            new MovementCommand(this.gameObject, _direction * Time.deltaTime * _moveSpeed);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }
}