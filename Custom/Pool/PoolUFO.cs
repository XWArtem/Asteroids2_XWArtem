using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

class PoolUFO : PoolBase
{
    public static List<GameObject> _ufoList = new List<GameObject>();
    private PoolUFOCounter _poolUFOCounter;

    public static CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
    CancellationToken cancelToken = cancelTokenSource.Token;

    private bool isActive = true;

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
            if (item.gameObject != null &&
                !item.gameObject.activeInHierarchy &&
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
            if (!_poolUFOCounter._UFOOnScene.Contains(i) && isActive)
            {
                int timeToSpawn = _randomTimeGenerator.GetRandomTimerForSpawn();
                
                await Task.Run(async () => 
                {
                    await Task.Delay(timeToSpawn * 100);
                    if (cancelToken.IsCancellationRequested)
                    {
                        isActive = false;
                    }
                }, cancelToken);

                if (isActive)
                {
                    UFORespawn(i);
                }

                if (i >= GameConfig.MaxNumberOfUFO)
                {
                    i = 0;
                }
                else
                {
                    i++;
                }
            }
            else
            {
                int timeToSpawn = _randomTimeGenerator.GetRandomTimerForSpawn();
                await Task.Run(async () =>
                {
                    await Task.Delay(timeToSpawn * 100);
                    if (cancelToken.IsCancellationRequested)
                    {
                        isActive = false;
                    }
                }, cancelToken);

                if (cancelToken.IsCancellationRequested)
                {
                    isActive = false;
                }
            }
        }
    }

    public void ReturnToPool(int ufoIndex)
    {
        _ufoList[ufoIndex].SetActive(false);
        _poolUFOCounter._UFOOnScene.Remove(ufoIndex);
    }

    private void UFORespawn(int index)
    {
        var tempRouteCoordinates = base._randomGenerator.RandomPosRoute;

        GetFreeElement(tempRouteCoordinates[0].X,
            tempRouteCoordinates[0].Y,
            0,
            index,
            ConstStrings.UFONAME,
            PoolEntity.UFOEntitiesPool);
        _poolUFOCounter._UFOOnScene.Add(index);
    }
}

