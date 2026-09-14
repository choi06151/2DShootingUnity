using UnityEngine;

public class UI_AttackDamageUpgradeButton : UI_UpgradeButton
{
    protected override void EffectApply()
    {
        Player.UpgradeDamage(_effectAmount);
    }
}