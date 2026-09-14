using UnityEngine;

public class UI_ScoreGetAmountUpgradeButton : UI_UpgradeButton
{
    protected override void EffectApply()
    {
        ScoreManager.Instance.UpgradeScoreMultiplier(_effectAmount);
    }
}