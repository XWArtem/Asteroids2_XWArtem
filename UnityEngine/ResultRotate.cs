using UnityEngine;

public class ResultRotate : MonoBehaviour
{
    private void Awake()
    {
        AsteroidsPositionUpdate.RotateAsteroid += RotateAsteroid;
        MainHeroPositionUpdate.RotateMainHeroAction += RotateMainHero;
    }

    private void OnDisable()
    {
        AsteroidsPositionUpdate.RotateAsteroid -= RotateAsteroid;
        MainHeroPositionUpdate.RotateMainHeroAction -= RotateMainHero;
    }
    private void RotateMainHero(float newRotationAngle)
    {
        UnityEngineUpdate._mainHero.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotationAngle);
    }

    private void RotateAsteroid(int asteroidIndex, float newRotationAngle)
    {
        var asteroid = PoolAsteroid._asteroidList[asteroidIndex];
        asteroid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotationAngle);
    }

}
