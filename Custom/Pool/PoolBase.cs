using System;
using UnityEngine;
using System.Collections.Generic;

public abstract class PoolBase
{
    public RandomGenerator _randomGenerator = new RandomGenerator();
    public RandomTimeGenerator _randomTimeGenerator = new RandomTimeGenerator();
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
        Debug.Log("index is: " + index + " and element is: " + element);
        throw new Exception("Pool is empty");
    }

    public GameObject GetFreeElement
        (float startX, float startY, float angle, int index, string objectName, List<ObjectEntity> objectEntityList)
    {
        var element = GetFreeElement(index);
        element.transform.position = new Vector2(startX, startY);
        element.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        objectEntityList.Find
            (e => e.Name.Contains(objectName + index.ToString())).CurrentX = startX;
        objectEntityList.Find
            (e => e.Name.Contains(objectName + index.ToString())).CurrentY = startY;
        objectEntityList.Find
            (e => e.Name.Contains(objectName + index.ToString())).RotationAngle = angle;
        return element;
    }

    public abstract bool TryGetElement(out GameObject element, int index);
}

