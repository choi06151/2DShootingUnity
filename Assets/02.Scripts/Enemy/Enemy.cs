using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float MoveSpeed = 3f;
    private Vector3 _direction = new Vector3(0, -1, 0);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        MovementCommand movementCommand = new MovementCommand(this.gameObject,
            _direction * Time.deltaTime * MoveSpeed);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }
}