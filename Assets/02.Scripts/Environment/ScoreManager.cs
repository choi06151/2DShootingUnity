using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int _bestScore;
    private int _currentScore;

    [SerializeField] private TextMeshProUGUI _bestScoreText;

    [SerializeField] private TextMeshProUGUI _currentScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _bestScoreText.text = _bestScore.ToString();
        _currentScoreText.text = _currentScore.ToString();
    }
}