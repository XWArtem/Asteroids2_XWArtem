using UnityEngine;

public class ResultTransform : MonoBehaviour
{
    void Awake()
    {
        MainHeroPositionUpdate.TransformMainHeroAction += TransformMainHero;
        AsteroidsPositionUpdate.TransformAsteroid += TransformAsteroid;
        BulletPositionUpdate.TransformBullet += TransformBullet;
    }

    private void OnDisable()
    {
        MainHeroPositionUpdate.TransformMainHeroAction -= TransformMainHero;
        AsteroidsPositionUpdate.TransformAsteroid -= TransformAsteroid;
        BulletPositionUpdate.TransformBullet -= TransformBullet;
    }

    private void TransformMainHero(float newX, float newY)
    {
        UnityEngineUpdate._mainHero.transform.position = new Vector2(newX, newY);
    }

    private void TransformAsteroid(int asteroidIndex, float newX, float newY)
    {
        var asteroid = PoolAsteroid._asteroidList[asteroidIndex];
        asteroid.transform.position = new Vector2(newX, newY);
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
}
