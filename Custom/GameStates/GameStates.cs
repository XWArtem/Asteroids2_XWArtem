public class GameStates
{
    public enum GameState
    {
        MainMenu,
        PlayMode,
        GameOver,
        ScoreScreen
    }

    public static GameState previousGameState;
    public static GameState newGameState;

    public delegate void GameStateChanged(GameState newState);
    public static event GameStateChanged OnGameStateChanged;

    public static void ChangeGameState(GameState newGameState)
    {
        if (OnGameStateChanged != null)
        {
            OnGameStateChanged(newGameState);
        }
        previousGameState = newGameState;
    }

}
