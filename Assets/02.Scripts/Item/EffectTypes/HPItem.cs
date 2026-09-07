using UnityEngine;

public class HPItem : ItemEffector
{
    [Header("체력 증가량")] [SerializeField] float _hpUpAmount;


    public override void Effect(Player player)
    {
        player.TakeHp(_hpUpAmount);
    }
}