using System;
using UnityEngine;

[RequireComponent(typeof(ItemMove))]
public class Item : PoolingObject
{
    [Header("아이템 이동속도")] [SerializeField] private float _moveSpeed;

    [Header("아이템 출현 후 대기시간")] [SerializeField]
    private float _waitTime;

    [Header("아이템 획득시 VFX프리팹")] [SerializeField]
    protected GameObject _UseEffect;

    [Header("아이템 획득시 소리")] [SerializeField]
    protected AudioClip _effectAudio;

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
        if (other != null && other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player == null || _itemEffector == null)
                return;

            if (_UseEffect != null)
                Instantiate(_UseEffect, transform.position, Quaternion.identity);

            _itemEffector.Effect(player);
            //Destroy(gameObject);
            if (PoolManager.Instance != null)
                PoolManager.Instance.InputToPool(this.gameObject);
            if (AudioManager.Instance != null && _effectAudio != null)
                AudioManager.Instance.ActivateEffectAudioClip(_effectAudio);
            //this.GetComponent<PoolingObject>().Deactivate();
        }
    }
}
