using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _bestScore;
    private int _currentScore;
    public int BestScore => _bestScore;
    public int CurrentScore => _currentScore;
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
            return;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        _bestScoreText.text = $"Best score: {_bestScore}";
        _currentScoreText.text = $"current score: {_currentScore}";
    }

    public void Addscore(int score)
    {
        if (score <= 0) return;
        _currentScore += score;
        Refresh();

        UpdateBestScore();
    }

    private void UpdateBestScore()
    {
        if (_bestScore < _currentScore)
        {
            _bestScore = _currentScore;
            Refresh();
        }
    }
}