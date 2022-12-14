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
        PoolUFO._ufoList.Clear();

        PoolUFO.cancelTokenSource.Cancel();

        PoolEntity.AsteroidEntitiesPool.Clear();
        PoolEntity.BulletEntitiesPool.Clear();
        PoolEntity.UFOEntitiesPool.Clear();
        PoolEntity.SmallAsteroidEntitiesPool.Clear();
        PoolEntity.UFOEntitiesPool.Clear();

        ScoreRepository.CurrentScore = 0;

        SceneManager.LoadScene(0);

        GameStates.ChangeGameState(GameStates.GameState.PlayMode);
        
    }
    public void Quit()
    {
        PoolUFO.cancelTokenSource.Cancel();
        Application.Quit();
    }
}
