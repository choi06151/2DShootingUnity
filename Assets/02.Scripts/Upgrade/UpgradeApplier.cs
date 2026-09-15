using UnityEngine;

public abstract class UpgradeApplier : MonoBehaviour
{
    [Header("현재 다뤄지는 업그레이드 정보")]
    [SerializeField] UpgradeDataSO _upgradeData;

    public UpgradeDataSO upgradeData => _upgradeData;

    private Player _player;
    public Player Player => _player;

    private int _level = 1;
    protected int _score;
    public int Score => _score;
    protected float _currentValue;
    public float CurrentValue => _currentValue;
    protected float _nextValue;
    public float NextValue => _nextValue;


    public void Init(Player player)
    {
        _player = player;
        _score = upgradeData._initScoreText;
        //Load();
        ApplyEffect();
    }

    public void TryUpgrade()
    {
        ScoreManager scoreManager = ScoreManager.Instance;
        if (scoreManager.IsScoreEnough(Score))
        {
            scoreManager.Usescore(Score);
            Upgrade();
        }
    }

    public void Upgrade()
    {
        _level++;
        _score *= (int)_upgradeData._scoreMultiplier;
        ApplyEffect();
        Save();
    }


    protected abstract void ApplyEffect();

    private void Save()
    {
        string json = JsonUtility.ToJson(_upgradeData);
        PlayerPrefs.SetString(_upgradeData._title, json);
        PlayerPrefs.Save();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey(_upgradeData._title)) return;

        string json = PlayerPrefs.GetString(_upgradeData._title);

        _upgradeData = JsonUtility.FromJson<UpgradeDataSO>(json);
    }
}