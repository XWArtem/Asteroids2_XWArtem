using UnityEngine;
using System;

public class ObjectSpawner: ObjectSpawnerBase
{
    private RandomGenerator _randomGenerator = new RandomGenerator();
    private bool _suscribedOnEvents;
    //private ObjectEntityRepository _objectEntityRepository = new ObjectEntityRepository();
    //public static Action<ObjectEntity> _createObjectEntityAction;
    
    private GameObject tempGameObject;
    public void SpawnMainHero()
    {
        ObjectEntity mainHeroEntity = new MainHeroEntity
            (ConstStrings.MainHeroName,
            0,
            SceneConfig.MainHeroSpawnPositionCoordinateX,
            SceneConfig.MainHeroSpawnPositionCoordinateY,
            0f,
            ConstStrings.MainHeroSpritePath,
            EntityType.mainHero);

        ObjectEntityRepository.CreateObjectEntity(mainHeroEntity);

        Spawn(mainHeroEntity);
        if (_suscribedOnEvents)
        {
            return;
        }
        MainHeroWeapon.bulletSpawnAction += SpawnBullet;
        _suscribedOnEvents = true;
    }
    public void SpawnAsteroid(int asteroidIndex)
    {
        var tempRouteCoordinates = _randomGenerator.RandomPosRoute;
        bool tempRotateLeft = _randomGenerator.RotateLeftRandom;

        ObjectEntity AsteroidEntity = new AsteroidEntity
            (ConstStrings.AsteroidName + asteroidIndex.ToString(),
            tempRouteCoordinates[0].X,
            tempRouteCoordinates[0].Y,
            tempRouteCoordinates[0].X,
            tempRouteCoordinates[0].Y,
            tempRouteCoordinates[1].X,
            tempRouteCoordinates[1].Y,
            0f,
            tempRotateLeft,
            ConstStrings.AsteroidSpritePath,
            EntityType.asteroid);

        ObjectEntityRepository.CreateObjectEntity(AsteroidEntity);

        Spawn(AsteroidEntity);

    }
    public void SpawnBullet(float startX, float startY, float angle, int bulletIndex)
    {
        ObjectEntity BulletEntity = new BulletEntity
            ( ConstStrings.BulletName + bulletIndex.ToString(),
            bulletIndex,
            startX,
            startY,
            angle,
            ConstStrings.BulletSpritePath,
            EntityType.bullet);

        ObjectEntityRepository.CreateObjectEntity(BulletEntity);

        Spawn(BulletEntity);

    }
    public void SpawnUFO()
    {

    }
    public override GameObject Spawn(ObjectEntity objectEntity)
    {
        tempGameObject = new GameObject();
        tempGameObject.name = objectEntity.Name;
        tempGameObject.transform.position = new Vector2(objectEntity.CurrentX, objectEntity.CurrentY);
        tempGameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, objectEntity.RotationAngle);
        tempGameObject.AddComponent<SpriteRenderer>();
        tempGameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(objectEntity.ImagePath);
        if (objectEntity._entityType == EntityType.mainHero)
        {
            UnityEngineUpdate.Instance.MainHero = tempGameObject;
        }
        else if (objectEntity._entityType == EntityType.asteroid)
        {
            UnityEngineUpdate.Instance.Asteroids.Add(tempGameObject);
        }
        else if (objectEntity._entityType == EntityType.bullet)
        {
            UnityEngineUpdate.Instance.Bullets.Add(tempGameObject);
        }
        return tempGameObject;
    }
}
