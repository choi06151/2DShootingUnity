using UnityEngine;

public class ForwardBulletMove : BulletMove
{
    protected override void Move()
    {
        MovementCommand movementCommand =
            new MovementCommand(this.gameObject, Vector2.up * _moveSpeed * Time.deltaTime);
        CommandManager.Instance.ExecuteCommand(movementCommand);
        Debug.Log("이동!");
    }
}