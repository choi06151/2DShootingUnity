using System;
using UnityEngine;

[RequireComponent(typeof(ItemMove))]
public class Item : PoolingObject
{
    private float _moveSpeed;
    private float _waitTime;

    public float MoveSpeed => _moveSpeed;
    public float WaitTime => _waitTime;

    private ItemCreator _itemCreator;
    private ItemMove _itemMove;
    private ItemEffector _itemEffector;

    public ItemCreator GetItemCreator => _itemCreator;

    public void InitItem(ItemCreator itemCreator)
    {
        _itemCreator = itemCreator;
        _moveSpeed = itemCreator.ItemMoveSpeed;
        _waitTime = itemCreator.ItemWaitTime;

        _itemMove = GetComponent<ItemMove>();
        _itemMove.Init(this);
        _itemEffector = GetComponent<ItemEffector>();
    }


    private void Update()
    {
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _itemEffector.Effect(other.GetComponent<Player>());
            //Destroy(gameObject);
            PoolManager.Instance.InputToPool(this.gameObject);

            //this.GetComponent<PoolingObject>().Deactivate();
        }
    }
}