using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_UpgradeButton : UI_ButtonParent
{
    protected TextMeshProUGUI _titleText;

    protected TextMeshProUGUI _conTextText;

    protected TextMeshProUGUI _scoreCostText;

    [Header("효과 내용")]
    [SerializeField] protected string _title;

    [Header("최초 가격")]
    [SerializeField] protected int _initScoreText;

    [Header("가격 증가량(배율)")]
    [SerializeField] protected int _scoreMultiplier;

    [Header("효과 (배율)")]
    [SerializeField] protected int _effectAmount;

    private int _upgradeLevel = 1;

    protected override void InitExecute()
    {
        _titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();
        _conTextText = transform.Find("ContextText").GetComponent<TextMeshProUGUI>();
        _scoreCostText = transform.Find("ScoreCostText").GetComponent<TextMeshProUGUI>();
        Refresh();
    }

    protected override void ClickExecute()
    {
        Debug.Log(" 업그레이드");
        ScoreManager scoreManager = ScoreManager.Instance;
        if (scoreManager.IsScoreEnough(_initScoreText))
        {
            scoreManager.Usescore(_initScoreText);
            UpgradeApply();
        }
    }

    private void UpgradeApply()
    {
        _upgradeLevel++;
        _initScoreText *= _scoreMultiplier;
        EffectApply();
        Refresh();
    }

    protected abstract void EffectApply();

    private void Refresh()
    {
        _titleText.text = _title;
        _conTextText.text = _upgradeLevel.ToString();
        _scoreCostText.text = _initScoreText.ToString();
    }
}