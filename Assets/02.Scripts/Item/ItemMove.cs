using UnityEngine;

public class ItemMove : MonoBehaviour, IItemFun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Player _player;
    private CommandManager _commandManager;
    private float _moveSpeed;
    private float _itemWaitTime;
    private bool _isInit = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (IsEnableStart())
        {
            MoveToPlayer();
        }
    }

    public void Init(Item item)
    {
        _player = item.GetItemCreator.GetPlayer;
        _commandManager = item.GetItemCreator.GetCommandManager;
        _moveSpeed = item.MoveSpeed;
        _itemWaitTime = item.WaitTime;
        _isInit = true;
    }

    private bool IsEnableStart()
    {
        _itemWaitTime -= Time.deltaTime;

        return _itemWaitTime <= 0 && _isInit;
    }

    private void MoveToPlayer()
    {
        Vector3 playerPosition = _player.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;

        MovementCommand movementCommand = new MovementCommand(this.gameObject, direction * _moveSpeed);
        _commandManager.ExecuteCommand(movementCommand);
    }
}