using UnityEngine;

public class DamageUpgradeApplier : UpgradeApplier
{
    protected override void ApplyEffect()
    {
        Player.UpgradeDamage(upgradeData._effectAmount);
        _currentValue = Player.Stat.Damage;
        _nextValue = _currentValue * upgradeData._effectAmount;
    }
}