using System;
using UnityEngine;

public abstract class PoolBase
{
    public RandomGenerator _randomGenerator = new RandomGenerator();
    public GameObject tempGameObject;

    public abstract void FillPool(int capacity);

    public GameObject CreateElement(ObjectEntity objectEntity)
    {
        tempGameObject = new GameObject();
        tempGameObject.name = objectEntity.Name;
        tempGameObject.transform.position = new Vector2(objectEntity.StartX, objectEntity.StartY);
        tempGameObject.transform.rotation = Quaternion.Euler(0f, 0f, objectEntity.RotationAngle);
        tempGameObject.AddComponent<SpriteRenderer>();
        tempGameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(objectEntity.ImagePath);

        return tempGameObject;
    }

    public GameObject GetFreeElement(int index)
    {
        if (TryGetElement(out var element, index))
        {
            return element;
        }
        throw new Exception("Pool is empty");
    }

    public GameObject GetFreeElement(float startX, float startY, float angle, int index)
    {
        var element = GetFreeElement(index);
        element.transform.position = new Vector2(startX, startY);
        element.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        //element.name = ConstStrings.BULLETNAME + index.ToString();
        
        EntityPool.BulletEntitiesPool.Find
            (e => e.Name.Contains(ConstStrings.BULLETNAME + index.ToString())).CurrentX = startX;
        EntityPool.BulletEntitiesPool.Find
            (e => e.Name.Contains(ConstStrings.BULLETNAME + index.ToString())).CurrentY = startY;
        EntityPool.BulletEntitiesPool.Find
            (e => e.Name.Contains(ConstStrings.BULLETNAME + index.ToString())).RotationAngle = angle;
        return element;
    }

    public abstract bool TryGetElement(out GameObject element, int index);
}

