using UnityEngine;
using System.Collections.Generic;

class PoolAsteroid : PoolBase
{
    public List<GameObject> _asteroidList = new List<GameObject>();

    public PoolAsteroid(int capacity)
    {
        FillPool(capacity);
    }

    public override void FillPool(int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            int index = i;
            var tempRouteCoordinates = base._randomGenerator.RandomPosRoute;
            bool tempRotateLeft = _randomGenerator.RotateLeftRandom;

            ObjectEntity asteroidEntity = new AsteroidEntity
                (ConstStrings.ASTEROIDNAME + index.ToString(),
                tempRouteCoordinates[0].X,
                tempRouteCoordinates[0].Y,
                tempRouteCoordinates[0].X,
                tempRouteCoordinates[0].Y,
                tempRouteCoordinates[1].X,
                tempRouteCoordinates[1].Y,
                0f,
                tempRotateLeft,
                ConstStrings.ASTEROIDSPRITEPATH,
                EntityType.asteroid);

            EntityPool.AsteroidEntitiesPool.Add(asteroidEntity);
            //_entityPool.FillAsteroidEntityPool(asteroidEntity);
            tempGameObject = CreateElement(asteroidEntity);
            _asteroidList.Add(tempGameObject);
        }
    }


    public override bool TryGetElement(out GameObject element, int asteroidIndex)
    {
        foreach (var item in _asteroidList)
        {
            if (!item.gameObject.activeInHierarchy)
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

}

