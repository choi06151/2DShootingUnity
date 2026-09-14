using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _bestScore;
    private int _currentScore;
    public int BestScore => _bestScore;
    public int CurrentScore => _currentScore;

    private float _scoreMultiplier = 1;
    public float ScoreMultiplier => _scoreMultiplier;


    private const string SaveKey = "BestScore";

    [SerializeField] private TextMeshProUGUI _bestScoreText;

    [SerializeField] private TextMeshProUGUI _currentScoreText;


    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            _bestScore = PlayerPrefs.GetInt(SaveKey, 0);
        }


        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Refresh()
    {
        _bestScoreText.text = $"Best score: {_bestScore:N0}";
        _currentScoreText.text = $"current score: {_currentScore:N0}";
    }

    public void UpgradeScoreMultiplier(float input)
    {
        _scoreMultiplier *= input;
    }

    public void Addscore(int score)
    {
        if (score <= 0) return;
        _currentScore += score * (int)_scoreMultiplier;
        Refresh();
        UpdateBestScore();
    }

    public bool IsScoreEnough(int score)
    {
        if (score <= 0) return false;

        return _currentScore >= score;
    }

    public void Usescore(int score)
    {
        if (score <= 0) return;
        _currentScore -= score;

        Refresh();
    }

    private void UpdateBestScore()
    {
        if (_bestScore < _currentScore)
        {
            PlayerPrefs.SetInt(SaveKey, _bestScore);
            PlayerPrefs.Save();

            _bestScore = _currentScore;
            Refresh();
        }
    }
}