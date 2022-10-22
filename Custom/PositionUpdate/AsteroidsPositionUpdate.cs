using System;
using System.Collections.Generic;

public class AsteroidsPositionUpdate 
{
    public static Action<List<ObjectEntity>, int, float, float> TransformAsteroidAction;
    public static Action<List<ObjectEntity>, int, float> RotateAsteroidAction;

    private bool rotateLeft;

    private float currentAngle;
    private float angleDelta;

    private float currentX;
    private float currentY;
    private float startX;
    private float startY;
    private float destinationX;
    private float destinationY;
    private float speed;

    private float directionX;
    private float directionY;

    private float deltaX;
    private float deltaY;

    private float newX;
    private float newY;
    private float newAngle;

    private float rotationSpeed = 20f;

    public void TransformAsteroid(int index)
    {
        Move(PoolEntity.AsteroidEntitiesPool, index, ConstStrings.ASTEROIDNAME);
        Rotate(PoolEntity.AsteroidEntitiesPool, index, ConstStrings.ASTEROIDNAME);
    }

    public void TransformSmallAsteroid(int index)
    {
        Move(PoolEntity.SmallAsteroidEntitiesPool, index, ConstStrings.SMALLASTEROIDNAME);
        Rotate(PoolEntity.SmallAsteroidEntitiesPool, index, ConstStrings.SMALLASTEROIDNAME);
    }

    public float[] GetEntityValues(List<ObjectEntity> poolEntityList, string entityName)
    {
        var tempEntity = poolEntityList.Find(e => e.Name.Contains(entityName));
        float[] values = new float[8];
        values[0] = tempEntity.CurrentX;
        values[1] = tempEntity.CurrentY;
        values[2] = tempEntity.StartX;
        values[3] = tempEntity.StartY;
        values[4] = tempEntity.DestinationX;
        values[5] = tempEntity.DestinationY;
        values[6] = tempEntity.Speed;
        values[7] = tempEntity.RotationAngle;
        return values;
    }

    private void Move(List<ObjectEntity> poolEntityList, int index, string asteroidName)
    {
        var floatValues = GetEntityValues(poolEntityList, asteroidName + index);
        currentX = floatValues[0];
        currentY = floatValues[1];
        startX = floatValues[2];
        startY = floatValues[3];
        destinationX = floatValues[4];
        destinationY = floatValues[5];
        speed = floatValues[6];

        asteroidName = poolEntityList.Find
                (e => e.Name.Contains(asteroidName + index.ToString())).Name;

        directionX = (float)Math.Clamp((destinationX - startX)/10, -1.0, 1.0);
        directionY = (float)Math.Clamp((destinationY - startY)/10, -1.0, 1.0);

        deltaX = (float)Math.Clamp(directionX * speed, -0.02, +0.02);
        deltaY = (float)Math.Clamp(directionY * speed, -0.02, +0.02);

        newX = currentX + deltaX;
        newY = currentY + deltaY;

        if (MathF.Abs(newX) >= GameConfig.MaxAxisX + 3.0f)
        {
            newX *= (-1);
        }
        if (MathF.Abs(newY) >= GameConfig.MaxAxisY + 3.0f)
        {
            newY *= (-1);
        }

        poolEntityList.Find(e => e.Name.Contains(asteroidName)).CurrentX = newX;
        poolEntityList.Find(e => e.Name.Contains(asteroidName)).CurrentY = newY;

        TransformAsteroidAction?.Invoke(poolEntityList, index, newX, newY);
    }

    public void Rotate(List<ObjectEntity> poolEntityList, int index, string asteroidName)
    {
        currentAngle = poolEntityList.Find(e => e.Name.Contains(asteroidName + index.ToString())).RotationAngle;
        rotateLeft = poolEntityList.Find(e => e.Name.Contains(asteroidName + index.ToString())).RotateLeft;
        speed = poolEntityList.Find(e => e.Name.Contains(asteroidName + index.ToString())).Speed;

        angleDelta = (rotateLeft) ? rotationSpeed * GameConfig.AsteroidRotationForce * speed : -rotationSpeed * GameConfig.AsteroidRotationForce * speed;

        newAngle = currentAngle + angleDelta;
        poolEntityList.Find(e => e.Name.Contains(asteroidName + index.ToString())).RotationAngle = newAngle;

        RotateAsteroidAction?.Invoke(poolEntityList, index, newAngle);
    }
}
