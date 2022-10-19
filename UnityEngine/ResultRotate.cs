using UnityEngine;
using System.Collections.Generic;

public class ResultRotate : MonoBehaviour
{
    private GameObject tempAsteroid;
    private void Awake()
    {
        AsteroidsPositionUpdate.RotateAsteroidAction += RotateAsteroid;
        MainHeroPositionUpdate.RotateMainHeroAction += RotateMainHero;
    }

    private void OnDisable()
    {
        AsteroidsPositionUpdate.RotateAsteroidAction -= RotateAsteroid;
        MainHeroPositionUpdate.RotateMainHeroAction -= RotateMainHero;
    }
    private void RotateMainHero(float newRotationAngle)
    {
        UnityEngineUpdate._mainHero.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotationAngle);
    }

    private void RotateAsteroid(List<ObjectEntity> objectEntityList, int asteroidIndex, float newRotationAngle)
    {
        if (objectEntityList == PoolEntity.AsteroidEntitiesPool)
        {
            tempAsteroid = PoolAsteroid._asteroidList[asteroidIndex];
        }
        else
        {
            tempAsteroid = PoolSmallAsteroid._smallAsteroidList[asteroidIndex];
        }
        tempAsteroid.transform.rotation = Quaternion.Euler(0.0f, 0.0f, newRotationAngle);
    }
}
