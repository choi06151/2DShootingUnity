using UnityEngine;

[System.Serializable]
public class Stat
{
    public float MoveSpeed { get; set; }
    public float MoveSpeedMultiplier { get; set; }
    public float Hp { get; set; }
    public float Damage { get; set; }
    public float BulletCoolTime { get; set; }
    public float BulletInterval { get; set; }
    public int BulletCount { get; set; }
    public int BulletUpgradeCount { get; set; }
    public bool IsBulletAutoFireOn { get; set; }
    public bool IsPlayerAutoMoveOn { get; set; }
    public bool IsPlayerAutoSkillOn { get; set; }
    public float FollowerInterval { get; set; }
    public float PlayerYRangeMin { get; set; }
    public float PlayerYRangeMax { get; set; }
}