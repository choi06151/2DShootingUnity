using System;
using Unity.VisualScripting;
using UnityEngine;

public class ToPlayerDirectionEnemy : Enemy
{
    private Vector3 _direction;

    private void Start()
    {
        _direction = (CommandManager.Instance.PlayerMove.transform.position - transform.position).normalized;
    }

    protected override void Move()
    {
        MovementCommand movementCommand = new MovementCommand(this.gameObject, _direction * Time.deltaTime * MoveSpeed);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }
}