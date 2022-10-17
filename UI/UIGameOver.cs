using UnityEngine;
using TMPro;
using System.Text;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverBackground;
    [SerializeField] private TextMeshProUGUI _scoreResultText;
    [SerializeField] private TextMeshProUGUI _bestResultText;

    private void OnEnable()
    {
        GameStates.OnGameStateChanged += GameOverMenuOpen;
    }
    private void OnDisable()
    {
        GameStates.OnGameStateChanged -= GameOverMenuOpen;
    }

    private void GameOverMenuOpen(GameStates.GameState gameState)
    {
        if (gameState == GameStates.GameState.GameOver)
        {
            var builder = new StringBuilder();
            builder.Append("You scored: ");
            builder.Append(ScoreRepository.CurrentScore.ToString());
            builder.Append("\nYour best result: ");
            builder.Append(ScoreRepository.MaxScore.ToString());

            _bestResultText.text = builder.ToString();

            _gameOverBackground.SetActive(true);
        }
        else
        {
            _gameOverBackground.SetActive(false);
        }
    }
}
