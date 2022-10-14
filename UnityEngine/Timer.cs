using UnityEngine;

class Timer : MonoBehaviour
{
    private void OnEnable()
    {
        GameStates.OnGameStateChanged += GameOverTimer;
        GameStates.OnGameStateChanged += PlayModeEnterTimer;
    }

    private void OnDisable()
    {
        GameStates.OnGameStateChanged -= GameOverTimer;
        GameStates.OnGameStateChanged -= PlayModeEnterTimer;
    }

    private void GameOverTimer(GameStates.GameState gameState)
    {
        if (gameState == GameStates.GameState.GameOver)
        {
            Time.timeScale = 0.0f;
        }
    }

    private void PlayModeEnterTimer(GameStates.GameState gameState)
    {
        if (gameState == GameStates.GameState.PlayMode)
        {
            Time.timeScale = 1.0f;
        }
    }
}