using UnityEngine;

public class BulletTypePlusItem : ItemEffector
{
    [Header("추가되는 총알 종류")] [SerializeField]
    BulletMove _bulletMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Effect(Player player)
    {
        player.TakeBulletTypePlusItem(_bulletMove);
    }
}