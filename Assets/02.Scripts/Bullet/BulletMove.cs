using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float Damage = 35f;
    private bool _isActive;

    private Player _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
        {
            Move();
        }
    }


    private void Move()
    {
        MovementCommand movementCommand = new MovementCommand(this.gameObject, Vector2.up * MoveSpeed * Time.deltaTime);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }

    public void Init()
    {
        _isActive = false;
    }

    public void Activate(Vector3 pos)
    {
        transform.position = pos;
        _isActive = true;
    }

    public void Deactivate()
    {
        _isActive = false;
    }
}