using UnityEngine;
using UnityEngine.Serialization;

public class BulletTypePlusItem : ItemEffector
{
    [Header("추가되는 총알 종류")] [SerializeField]
    private Bullet _bullet;


    public override void Effect(Player player)
    {
        player.TakeBulletTypePlusItem(_bullet);
    }
}