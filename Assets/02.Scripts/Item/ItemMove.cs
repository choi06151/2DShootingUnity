using UnityEngine;

public class ItemMove : MonoBehaviour, IItemFun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player _player;
    private CommandManager _commandManager;
    private float _moveSpeed;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    public void Init(Item item)
    {
        _player = item.GetItemCreator.GetPlayer;
        _commandManager = item.GetItemCreator.GetCommandManager;
        _moveSpeed = item.ItemMoveSpeed;
    }

    private void MoveToPlayer()
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;

        MovementCommand movementCommand = new MovementCommand(this.gameObject, direction * _moveSpeed);
        _commandManager.ExecuteCommand(movementCommand);
    }
}