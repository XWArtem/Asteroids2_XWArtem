using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    public void Restart()
    {
        PoolAsteroid._asteroidList.Clear();
        PoolBullet._bulletList.Clear();
        PoolUFO._ufoList.Clear();
        PoolSmallAsteroid._smallAsteroidList.Clear();

        PoolEntity.AsteroidEntitiesPool.Clear();
        PoolEntity.BulletEntitiesPool.Clear();
        PoolEntity.UfoEntitiesPool.Clear();
        PoolEntity.SmallAsteroidEntitiesPool.Clear();

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
