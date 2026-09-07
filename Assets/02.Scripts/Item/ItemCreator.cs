using System.Collections.Generic;
using UnityEngine;


public class ItemCreator : MonoBehaviour
{
    [Header("아이템 리스트")] [SerializeField] private List<Item> _items;
    [Header("아이템 이동 속도")] [SerializeField] private float _itemMoveSpeed;
    
    [Header("아이템 이동 대기시간")] [SerializeField]
    private float _itemWaitTime;

    public float ItemMoveSpeed => _itemMoveSpeed;
    public float ItemWaitTime => _itemWaitTime;

    private CommandManager _commandManager;
    public CommandManager GetCommandManager => _commandManager;
    [SerializeField] Player _player;
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
        _commandManager = CommandManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CreateItem(Vector3 position)
    {
        if (IsCreate())
        {
            CreateCommand createCommand =
                new CreateCommand(this.gameObject, _items[GetRandomItemIdx()].gameObject, position);
            CommandManager.Instance.ExecuteCommand(createCommand);
            createCommand.GetCreatedObeject.GetComponent<Item>().InitItem(this);
        }
    }


    private bool IsCreate()
    {
        int randomIndex = Random.Range(0, 10);

        return randomIndex >= 7;
    }

    private int GetRandomItemIdx() //추후 확률 보정 
    {
        return Random.Range(0, _items.Count);
    }
}