using UnityEngine;

public class BulletCountItem : ItemEffector
{
    [Header("총알 발사 개수 증가율")] [SerializeField]
    private int _bulletCountPlus;

    public override void Effect(Player player)
    {
        player.TakeBulletCountUp(_bulletCountPlus);
    }
}