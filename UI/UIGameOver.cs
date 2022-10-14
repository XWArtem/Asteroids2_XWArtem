using UnityEngine;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverBackground;

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
            _gameOverBackground.SetActive(true);
        }
        else
        {
            _gameOverBackground.SetActive(false);
        }
    }
}
