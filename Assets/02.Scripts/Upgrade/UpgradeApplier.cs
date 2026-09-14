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
        ApplyEffect();
    }

    public void Upgrade()
    {
        _level++;
        _score *= (int)_upgradeData._scoreMultiplier;
        ApplyEffect();
    }


    protected abstract void ApplyEffect();
}