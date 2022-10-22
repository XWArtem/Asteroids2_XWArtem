using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

class PoolUFO : PoolBase
{


    public static List<GameObject> _ufoList = new List<GameObject>();
    private PoolUFOCounter _poolUFOCounter;

    public PoolUFO(int capacity)
    {
        FillPool(capacity);
        _poolUFOCounter = new PoolUFOCounter();
        TryUFORespawn();
    }

    public override void FillPool(int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            int index = i;
            var tempRouteCoordinates = base._randomGenerator.RandomPosRoute;
            float speed = base._randomGenerator.GetRandomSpeed(EntityType.UFO);

            ObjectEntity ufoEntity = new UfoEntity
                (ConstStrings.UFONAME + index.ToString(),
                tempRouteCoordinates[0].X,
                tempRouteCoordinates[0].Y,
                tempRouteCoordinates[0].X,
                tempRouteCoordinates[0].Y,
                0,
                0,
                speed,
                0f,
                false,
                ConstStrings.UFOSPRITEPATH,
                EntityType.UFO);

            PoolEntity.UFOEntitiesPool.Add(ufoEntity);
            base.tempGameObject = CreateElement(ufoEntity);
            base.tempGameObject.SetActive(false);
            _ufoList.Add(base.tempGameObject);
        }
    }

    public override bool TryGetElement(out GameObject element, int index)
    {
        foreach (var item in _ufoList)
        {
            if (!item.gameObject.activeInHierarchy &&
                item.gameObject.name.Contains(index.ToString()))
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public async void TryUFORespawn()
    {
        for (int i = 0; i < GameConfig.MaxNumberOfUFO;)
        {
            if (!_poolUFOCounter._UFOOnScene.Contains(i))
            {
                int timeToSpawn = _randomTimeGenerator.GetRandomTimerForSpawn();
                await Task.Delay(timeToSpawn * 100);

                var tempRouteCoordinates = base._randomGenerator.RandomPosRoute;

                GetFreeElement(tempRouteCoordinates[0].X,
                    tempRouteCoordinates[0].Y,
                    0,
                    i,
                    ConstStrings.UFONAME,
                    PoolEntity.UFOEntitiesPool);
                _poolUFOCounter._UFOOnScene.Add(i);

                i++;

                if (i > GameConfig.MaxNumberOfUFO - 1)
                {
                    i = 0;
                }
            }
            else
            {
                int timeToSpawn = _randomTimeGenerator.GetRandomTimerForSpawn();
                await Task.Delay(timeToSpawn * 100);
            }
        }
    }

    public void ReturnToPool(int ufoIndex)
    {
        _ufoList[ufoIndex].SetActive(false);
        _poolUFOCounter._UFOOnScene.Remove(ufoIndex);
    }
}

