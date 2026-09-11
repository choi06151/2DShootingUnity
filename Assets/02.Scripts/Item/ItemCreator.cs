using System.Collections.Generic;
using UnityEngine;


public class ItemCreator : MonoBehaviour
{
    [Header("아이템 리스트")] [SerializeField] private ItemSpawnDataTableSO _itemSpawnDataTableSo;
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
        if (IsCreate() && _itemSpawnDataTableSo != null && CommandManager.Instance != null)
        {
            GameObject itemPrefab = GetRandomItemIdx();
            if (itemPrefab == null)
                return;

            CreateCommand createCommand =
                new CreateCommand(this.gameObject, itemPrefab, position);
            CommandManager.Instance.ExecuteCommand(createCommand);
            if (createCommand.GetCreatedObeject != null &&
                createCommand.GetCreatedObeject.TryGetComponent(out Item item))
            {
                item.InitItem(this);
            }
        }
    }


    private bool IsCreate()
    {
        int randomIndex = Random.Range(0, 10);

        return randomIndex >= 7;
    }

    private GameObject GetRandomItemIdx() //추후 확률 보정 
    {
        int totalWeight = 0;
        foreach (var data in _itemSpawnDataTableSo.Datas)
        {
            totalWeight += data.Weight;
        }

        int randomWeight = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        foreach (var data in _itemSpawnDataTableSo.Datas)
        {
            cumulativeWeight += data.Weight;
            if (randomWeight < cumulativeWeight)
            {
                return data.ItemPrefab;
            }
        }

        return null;
    }
}