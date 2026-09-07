using UnityEngine;

public class BulletDamageUpItem : ItemEffector
{
    [Header("데미지 증가율")] [SerializeField] private float _damagePlusMultiplier;

    public override void Effect(Player player)
    {
        player.TakeDamageUp(_damagePlusMultiplier);
    }
}