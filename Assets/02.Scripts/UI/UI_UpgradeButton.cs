using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeButton : UI_ButtonParent
{
    protected TextMeshProUGUI _titleText;

    protected TextMeshProUGUI _conTextText;

    protected TextMeshProUGUI _scoreCostText;


    private UpgradeApplier _upgradeApplier;

    protected override void InitExecute()
    {
        _upgradeApplier = GetComponent<UpgradeApplier>();
        _titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        _conTextText = transform.Find("ContextText").GetComponent<TextMeshProUGUI>();
        _scoreCostText = transform.Find("ScoreCostText").GetComponent<TextMeshProUGUI>();
        Refresh();
    }

    protected override void ClickExecute()
    {
        ScoreManager scoreManager = ScoreManager.Instance;
        if (scoreManager.IsScoreEnough(_upgradeApplier.Score))
        {
            scoreManager.Usescore(_upgradeApplier.Score);
            _upgradeApplier.Upgrade();
            Refresh();
        }
    }


    private void Refresh()
    {
        _titleText.text = _upgradeApplier.upgradeData._title;
        _conTextText.text = _upgradeApplier.CurrentValue + " => " + _upgradeApplier.NextValue;
        _scoreCostText.text = _upgradeApplier.Score.ToString();
    }
}