using System.Collections.Generic;
using UnityEngine;
using System;

class PoolSmallAsteroid : PoolBase
{
    private PoolSmallAsteroidCounter _poolSmallAsteroidsCounter;

    public static List<GameObject> _smallAsteroidList = new List<GameObject>();
    public static Action<int> AsteroidSpawn;

    public PoolSmallAsteroid(int capacity)
    {
        FillPool(capacity);
        _poolSmallAsteroidsCounter = new PoolSmallAsteroidCounter();
        UnityEngineUpdate.smallAsteroidsSpawn += GetFreeSmallAsteroid;
    }

    public void DisableAction()
    {
        UnityEngineUpdate.smallAsteroidsSpawn -= GetFreeSmallAsteroid;
    }

    public override void FillPool(int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            int index = i;
            var tempRouteCoordinates = base._randomGenerator.RandomPosRoute;
            bool tempRotateLeft = _randomGenerator.RotateLeftRandom;
            float tempSpeed = _randomGenerator.GetRandomSpeed(EntityType.smallAsteroid);
            float tempRotation = _randomGenerator.RotationZ;

            ObjectEntity asteroidEntity = new SmallAsteroidEntity
                (ConstStrings.SMALLASTEROIDNAME + index.ToString(),
                tempRouteCoordinates[0].X,
                tempRouteCoordinates[0].Y,
                tempRouteCoordinates[0].X,
                tempRouteCoordinates[0].Y,
                tempRouteCoordinates[1].X,
                tempRouteCoordinates[1].Y,
                tempSpeed,
                tempRotation,
                tempRotateLeft,
                ConstStrings.SMALLASTEROIDSPRITEPATH,
                EntityType.smallAsteroid);

            PoolEntity.SmallAsteroidEntitiesPool.Add(asteroidEntity);
            tempGameObject = CreateElement(asteroidEntity);
            base.tempGameObject.SetActive(false);
            _smallAsteroidList.Add(tempGameObject);
        }
    }

    private void GetFreeSmallAsteroid(float startX, float startY, float angle, int index)
    {
        GetFreeElement(startX, startY, angle, index,
            ConstStrings.SMALLASTEROIDNAME, PoolEntity.SmallAsteroidEntitiesPool);
    }

    public override bool TryGetElement(out GameObject element, int index)
    {
        foreach (var item in _smallAsteroidList)
        {
            if (!item.gameObject.activeInHierarchy
                && item.name.Contains(ConstStrings.SMALLASTEROIDNAME + index.ToString()))
            {
                element = item;
                item.gameObject.SetActive(true);
                _poolSmallAsteroidsCounter._smallAsteroidsOnScene.Add(index);

                var tempCoordinates = base._randomGenerator.RandomPos;
                PoolEntity.SmallAsteroidEntitiesPool.Find
                    (e => e.Name.Contains(ConstStrings.SMALLASTEROIDNAME + index.ToString())).DestinationX = tempCoordinates.X;

                PoolEntity.SmallAsteroidEntitiesPool.Find
                    (e => e.Name.Contains(ConstStrings.SMALLASTEROIDNAME + index.ToString())).DestinationY = tempCoordinates.Y;

                return true;
            }
        }

        element = null;
        return false;
    }
    public void ReturnToPool(int smallAsteroidIndex)
    {
        _smallAsteroidList[smallAsteroidIndex].SetActive(false);
        _poolSmallAsteroidsCounter._smallAsteroidsOnScene.Remove(smallAsteroidIndex);

        if (!_poolSmallAsteroidsCounter.CheckSmallAsteroids(smallAsteroidIndex))
        {
            if (smallAsteroidIndex > GameConfig.NumberOfAsteroids - 1)
            {
                AsteroidSpawn?.Invoke(GameConfig.NumberOfSmallAsteroids - 1 - smallAsteroidIndex);
            }
            else
            {
                AsteroidSpawn?.Invoke(smallAsteroidIndex);
            }
        }
    }
}