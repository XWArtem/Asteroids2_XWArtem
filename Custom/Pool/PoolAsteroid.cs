using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

class PoolAsteroid : PoolBase
{
    public static List<GameObject> _asteroidList = new List<GameObject>();

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
    public void ReturnToPool(int asteroidIndex)
    {
        _asteroidList[asteroidIndex].SetActive(false);
        

        AsteroidRespawn(asteroidIndex);
    }

    private async void AsteroidRespawn(int asteroidIndex)
    {
        await Task.Delay(2000);
        var tempRouteCoordinates = base._randomGenerator.RandomPosRoute;
        bool tempRotateLeft = _randomGenerator.RotateLeftRandom;

        EntityPool.AsteroidEntitiesPool[asteroidIndex].CurrentX = tempRouteCoordinates[0].X;
        EntityPool.AsteroidEntitiesPool[asteroidIndex].CurrentY = tempRouteCoordinates[0].Y;

        EntityPool.AsteroidEntitiesPool[asteroidIndex].StartX = tempRouteCoordinates[0].X;
        EntityPool.AsteroidEntitiesPool[asteroidIndex].StartY = tempRouteCoordinates[0].Y;

        EntityPool.AsteroidEntitiesPool[asteroidIndex].DestinationX = tempRouteCoordinates[1].X;
        EntityPool.AsteroidEntitiesPool[asteroidIndex].DestinationY = tempRouteCoordinates[1].Y;

        EntityPool.AsteroidEntitiesPool[asteroidIndex].RotateLeft = tempRotateLeft;

        if (_asteroidList[asteroidIndex] != null)
        {
            _asteroidList[asteroidIndex].SetActive(true);
            _asteroidList[asteroidIndex].transform.position = new Vector2(tempRouteCoordinates[0].X, tempRouteCoordinates[0].Y);
            _asteroidList[asteroidIndex].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

}

