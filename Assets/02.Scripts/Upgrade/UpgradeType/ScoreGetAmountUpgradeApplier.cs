using UnityEngine;

public class ScoreGetAmountUpgradeApplier : UpgradeApplier
{
    protected override void ApplyEffect()
    {
        ScoreManager.Instance.UpgradeScoreMultiplier(upgradeData._effectAmount);

        _currentValue = ScoreManager.Instance.ScoreMultiplier;
        _nextValue = _currentValue * upgradeData._effectAmount;
    }
}