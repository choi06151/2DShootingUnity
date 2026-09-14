using UnityEngine;

public class SkillCoolTimeUpgradeApplier : UpgradeApplier
{
    protected override void ApplyEffect()
    {
        Player.UpgradeSkillCoolTime(upgradeData._effectAmount);
        _currentValue = Player.Stat.SkillCoolTimeRate;
        _nextValue = _currentValue * upgradeData._effectAmount;
    }
}