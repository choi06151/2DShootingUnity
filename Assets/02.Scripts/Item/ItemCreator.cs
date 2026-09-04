using System.Collections.Generic;
using UnityEngine;


enum ItemType
{
    SPEED,
    HEALTH,
    DAMAGE
}

public class ItemCreator : MonoBehaviour
{
    [Header("아이템 리스트")] [SerializeField] private List<Item> _items;


    private CommandManager _commandManager;
    public CommandManager GetCommandManager => _commandManager;
    private Player _player;
    public Player GetPlayer => _player;

    public static ItemCreator Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateItem(Vector3 position)
    {
        CreateCommand createCommand =
            new CreateCommand(this.gameObject, _items[(int)GetRandomItemIdx()].gameObject, position);
        CommandManager.Instance.ExecuteCommand(createCommand);
        createCommand.GetCreatedObeject.GetComponent<Item>().InitItem(this);
    }


    private ItemType GetRandomItemIdx() //추후 확률 보정 
    {
        return (ItemType)Random.Range(0, _items.Count);
    }
}