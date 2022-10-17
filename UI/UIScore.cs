using UnityEngine;
using TMPro;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private const string SCORE = "Score: ";

    private void OnEnable()
    {
        ScoreRepository.OnScoreChanged += ChangeScoreText;
        GameStates.OnGameStateChanged += ClearScoreText;
        _scoreText.text = SCORE + "0";
    }   
    private void OnDisable()
    {
        ScoreRepository.OnScoreChanged -= ChangeScoreText;
        GameStates.OnGameStateChanged -= ClearScoreText;
    }

    private void ChangeScoreText(int score)
    {
        _scoreText.text = SCORE + score.ToString();
    }

    private void ClearScoreText(GameStates.GameState gameState)
    {
        if (gameState == GameStates.GameState.GameOver)
        {
            _scoreText.text = "";
        }
    }
}
