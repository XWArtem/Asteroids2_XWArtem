//using UnityEngine;

//public class ObjectSpawner
//{
//    private RandomGenerator _randomGenerator = new RandomGenerator();
//    private GameObject tempGameObject;
    

//    private bool _suscribedOnEvents;
    
    

//    public void SpawnMainHero()
//    {
//        ObjectEntity mainHeroEntity = new MainHeroEntity
//            (ConstStrings.MAINHERONAME,
//            0,
//            SceneConfig.MainHeroSpawnPositionCoordinateX,
//            SceneConfig.MainHeroSpawnPositionCoordinateY,
//            0f,
//            ConstStrings.MAINHEROSPRITEPATH,
//            EntityType.mainHero);

//        EntityPool.CreateObjectEntity(mainHeroEntity);

//        _gameObjectPool.CreatePool(1, mainHeroEntity);

//        if (_suscribedOnEvents)
//        {
//            return;
//        }
//        MainHeroWeapon.bulletSpawnAction += SpawnBullet;
//        _suscribedOnEvents = true;
//    }
//    public void SpawnAsteroid()
//    {
//        for (int i = 0; i < SceneConfig.NumberOfAsteroids; i++)
//        {
//            int index = i;
//            var tempRouteCoordinates = _randomGenerator.RandomPosRoute;
//            bool tempRotateLeft = _randomGenerator.RotateLeftRandom;

//            ObjectEntity AsteroidEntity = new AsteroidEntity
//                (ConstStrings.ASTEROIDNAME + index.ToString(),
//                tempRouteCoordinates[0].X,
//                tempRouteCoordinates[0].Y,
//                tempRouteCoordinates[0].X,
//                tempRouteCoordinates[0].Y,
//                tempRouteCoordinates[1].X,
//                tempRouteCoordinates[1].Y,
//                0f,
//                tempRotateLeft,
//                ConstStrings.ASTEROIDSPRITEPATH,
//                EntityType.asteroid);
//            EntityPool.CreateObjectEntity(AsteroidEntity);
//        }
        
//        _gameObjectPool.CreatePool(SceneConfig.NumberOfAsteroids, AsteroidEntity);

//        Spawn(AsteroidEntity);
//    }

//    public void SpawnBullet(float startX, float startY, float angle, int bulletIndex)
//    {
//        ObjectEntity BulletEntity = new BulletEntity
//            ( ConstStrings.BULLETNAME + bulletIndex.ToString(),
//            bulletIndex,
//            startX,
//            startY,
//            angle,
//            ConstStrings.BULLETSPRITEPATH,
//            EntityType.bullet);

//        EntityPool.CreateObjectEntity(BulletEntity);

//        Spawn(BulletEntity);

//    }
//    public void SpawnUFO()
//    {

//    }
    
//}
