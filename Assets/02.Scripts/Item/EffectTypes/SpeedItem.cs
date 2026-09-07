using UnityEngine;

public class SpeedItem : ItemEffector
{
    [Header("속도 증가량")] [SerializeField] private float _speedPlusMultiplier;


    public override void Effect(Player player)
    {
        player.TakeSpeedUp(_speedPlusMultiplier);
    }
}