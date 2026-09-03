using UnityEngine;

public class MoveToDirection : MonoBehaviour
{
    private float _speed;
    private Vector3 _direction;

    public MoveToDirection(float speed, Vector3 direction)
    {
        _speed = speed;
        _direction = direction;
    }

    public void Move()
    {
        MovementCommand movementCommand = new MovementCommand(this.gameObject,
            _direction * Time.deltaTime * _speed);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }

    private void Update()
    {
        Move();
    }
}