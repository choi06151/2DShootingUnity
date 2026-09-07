using UnityEngine;

public class MaxHPItem : ItemEffector
{
    [Header("최대 체력 증가량")] [SerializeField] float _maxHpUpAmount;


    public override void Effect(Player player)
    {
        player.TakeMaxHp(_maxHpUpAmount);
    }
}