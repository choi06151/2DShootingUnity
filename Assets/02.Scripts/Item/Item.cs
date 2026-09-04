using System;
using UnityEngine;

[RequireComponent(typeof(ItemMove))]
public class Item : MonoBehaviour
{
    [Header("아이템 이동 속도")] [SerializeField] private float _itemMoveSpeed;
    public float ItemMoveSpeed => _itemMoveSpeed;


    private ItemCreator _itemCreator;
    public ItemCreator GetItemCreator => _itemCreator;

    public void InitItem(ItemCreator itemCreator)
    {
        _itemCreator = itemCreator;
    }


    private void Update()
    {
    }
}