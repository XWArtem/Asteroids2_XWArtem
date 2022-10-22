using UnityEngine;
using System.Collections.Generic;

public class ResultTransform : MonoBehaviour
{
    private GameObject tempAsteroid;
    void Awake()
    {
        MainHeroPositionUpdate.TransformMainHeroAction += TransformMainHero;
        AsteroidsPositionUpdate.TransformAsteroidAction += TransformAsteroid;
        BulletPositionUpdate.TransformBullet += TransformBullet;
        UFOPositionUpdate.TransformUFOAction += TransformUFO;
    }

    private void OnDisable()
    {
        MainHeroPositionUpdate.TransformMainHeroAction -= TransformMainHero;
        AsteroidsPositionUpdate.TransformAsteroidAction -= TransformAsteroid;
        BulletPositionUpdate.TransformBullet -= TransformBullet;
        UFOPositionUpdate.TransformUFOAction -= TransformUFO;
    }

    private void TransformMainHero(float newX, float newY)
    {
        UnityEngineUpdate._mainHero.transform.position = new Vector2(newX, newY);
    }

    private void TransformAsteroid(List<ObjectEntity> objectEntityList, int asteroidIndex, float newX, float newY)
    {
        {
            if (objectEntityList == PoolEntity.AsteroidEntitiesPool)
            {
                tempAsteroid = PoolAsteroid._asteroidList[asteroidIndex];
            }
            else
            {
                tempAsteroid = PoolSmallAsteroid._smallAsteroidList[asteroidIndex];
            }
            tempAsteroid.transform.position = new Vector2(newX, newY);
        }


        //var asteroid = PoolAsteroid._asteroidList[asteroidIndex];
        //asteroid.transform.position = new Vector2(newX, newY);
    }

    private void TransformBullet(int bulletIndex, float newX, float newY, bool isOutOfRange)
    {
        var bullet = PoolBullet._bulletList[bulletIndex];
        bullet.transform.position = new Vector2(newX, newY);

        if (isOutOfRange)
        {
            PoolBullet.ReturnToPool(bulletIndex);
        }
    }

    private void TransformUFO(List<ObjectEntity> objectEntityList, int UFOIndex, float newX, float newY)
    {
        tempAsteroid = PoolUFO._ufoList[UFOIndex];
        tempAsteroid.transform.position = new Vector2(newX, newY);
    }
}
