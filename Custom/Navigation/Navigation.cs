using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void Restart()
    {
        PoolAsteroid._asteroidList.Clear();
        PoolBullet._bulletList.Clear();
        PoolUFO._ufoList.Clear();

        EntityPool.AsteroidEntitiesPool.Clear();
        EntityPool.BulletEntitiesPool.Clear();
        EntityPool.UfoEntitiesPool.Clear();

        ScoreRepository.CurrentScore = 0;

        SceneManager.LoadScene(0);

        GameStates.ChangeGameState(GameStates.GameState.PlayMode);
    }
    public void Quit()
    {
        // save data
        Application.Quit();
    }
}
