using UnityEngine;
using System.Collections.Generic;

class PoolUFO : PoolBase 
{
    public static List<GameObject> _ufoList = new List<GameObject>();

    public PoolUFO(int capacity)
    {

        FillPool(capacity);
    }

    public override void FillPool(int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            int index = i;
            var tempRouteCoordinates = base._randomGenerator.RandomPosRoute;

            ObjectEntity ufoEntity = new UfoEntity
                (ConstStrings.UFONAME + index.ToString(),
                0f,
                0f,
                0f,
                0f,
                0f,
                0f,
                0f,
                0f,
                false,
                ConstStrings.UFOSPRITEPATH,
                EntityType.UFO); ;

            EntityPool.UfoEntitiesPool.Add(ufoEntity);
            //_entityPool.FillUfoEntityPool(ufoEntity);
            base.tempGameObject = CreateElement(ufoEntity);
            base.tempGameObject.SetActive(false);
            _ufoList.Add(base.tempGameObject);
        }
    }

    public override bool TryGetElement(out GameObject element, int index)
    {
        foreach (var item in _ufoList)
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

