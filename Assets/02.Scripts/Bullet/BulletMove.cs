using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float MoveSpeed = 3f;
    public float Damage = 35f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Init(Player player)
    {
        Damage = Damage * player.PlayerDamageMultiplier;
    }

    private void Move()
    {
        MovementCommand movementCommand = new MovementCommand(this.gameObject, Vector2.up * MoveSpeed * Time.deltaTime);
        CommandManager.Instance.ExecuteCommand(movementCommand);
    }
}