using System;
using UnityEngine;

public class GameObjectPool
{
    //public RandomGenerator _randomGenerator = new RandomGenerator();
    //private EntityPool _entityPool = new EntityPool();

    //private GameObject tempGameObject;

    //public GameObject MainHero;
    //private PoolAsteroid _poolAsteroid;
    //private PoolBullet _poolBullet;
    //private PoolUFO _poolUFO;
    //private PoolMainHero _poolMainHero;

    //public GameObjectPool(int asteroidsAmount, int bulletsAmount, int ufoAmount)
    //{
    //    _poolAsteroid = new PoolAsteroid(asteroidsAmount);
    //    _poolBullet = new PoolBullet(bulletsAmount);
    //    _poolUFO = new PoolUFO(ufoAmount);
    //    _poolMainHero = new PoolMainHero();
    //}

    //private GameObject CreateElement(ObjectEntity objectEntity)
    //{
    //    tempGameObject = new GameObject();
    //    tempGameObject.name = objectEntity.Name;
    //    tempGameObject.transform.position = new Vector2(objectEntity.StartX, objectEntity.StartY);
    //    tempGameObject.transform.rotation = Quaternion.Euler(0f, 0f, objectEntity.RotationAngle);
    //    tempGameObject.AddComponent<SpriteRenderer>();
    //    tempGameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(objectEntity.ImagePath);

    //    return tempGameObject;
    //}

    //private void SetMainHero()
    //{
    //    ObjectEntity mainHeroEntity = new MainHeroEntity
    //        (ConstStrings.MAINHERONAME,
    //        0,
    //        GameConfig.MainHeroSpawnPositionCoordinateX,
    //        GameConfig.MainHeroSpawnPositionCoordinateY,
    //        0f,
    //        ConstStrings.MAINHEROSPRITEPATH,
    //        EntityType.mainHero);

    //    CreateElement(mainHeroEntity);

    //    MainHero = tempGameObject;
    //    _entityPool.FillMainHero(mainHeroEntity);
    //}

    //public GameObject GetFreeElement()
    //{
    //    if (TryGetElement(out var element))
    //    {
    //        return element;
    //    }
    //    throw new Exception("Pool is empty");
    //}

    //private bool TryGetElement(out GameObject element)
    //{
    //    foreach (var item in _bulletPool)
    //    {
    //        if (!item.gameObject.activeInHierarchy)
    //        {
    //            element = item;
    //            item.gameObject.SetActive(true);
    //            return true;
    //        }
    //    }
    //    element = null;
    //    return false;
    //}

    //public bool TryGetElement(out GameObject element)
    //{
    //    foreach (var item in _pool)
    //    {
    //        if (!item.gameObject.activeInHierarchy)
    //        {
    //            element = item;
    //            item.gameObject.SetActive(true);
    //            return true;
    //        }
    //    }

    //    element = null;
    //    return false;
    //}
    //public GameObject GetFreeElement()
    //{
    //    if (TryGetElement(out var element))
    //    {
    //        return element;
    //    }
    //    if (_autoExpand)
    //    {
    //        return CreateElement(true);
    //    }
    //    if (_pool.Count < _maxCapacity)
    //    {
    //        return CreateElement(true);
    //    }
    //    throw new Exception("Pool is empty");
    //}

    //public GameObject GetFreeElement(PairOfFloats position, Quaternion rotation)
    //{
    //    var element = GetFreeElement();
    //    element.transform.position = position;
    //    element.transform.rotation = rotation;
    //    return element;
    //}
    //public GameObject GetFreeElement(PairOfFloats position)
    //{
    //    var element = GetFreeElement();
    //    element.transform.position = position;
    //    return element;
    //}


}
