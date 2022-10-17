using System;

public class AsteroidsPositionUpdate 
{
    public static Action<int, float, float> TransformAsteroid;
    public static Action<int, float> RotateAsteroid;

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

    private float deltaX;
    private float deltaY;

    private string asteroidName;
    private float newX;
    private float newY;
    private float newAngle;


    private float rotationSpeed = 0.01f;

    public void Transform(int index)
    {
        Move(index);
        Rotate(index);
    }

    public float[] GetEntityValues(string entityName)
    {
        var tempEntity = EntityPool.AsteroidEntitiesPool.Find(e => e.Name.Contains(entityName));
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

    private void Move(int index)
    {
        var floatValues = GetEntityValues(ConstStrings.ASTEROIDNAME + index);
        currentX = floatValues[0];
        currentY = floatValues[1];
        startX = floatValues[2];
        startY = floatValues[3];
        destinationX = floatValues[4];
        destinationY = floatValues[5];
        speed = floatValues[6];

        asteroidName = EntityPool.AsteroidEntitiesPool.Find
                (e => e.Name.Contains(ConstStrings.ASTEROIDNAME + index.ToString())).Name;

            deltaX = (float)Math.Clamp((destinationX - startX) * speed, -0.02, +0.02);
            deltaY = (float)Math.Clamp((destinationY - startY) * speed, -0.02, +0.02);

            newX = currentX + deltaX;
            newY = currentY + deltaY;

            if (MathF.Abs( newX) >= 12.2f)
            {
                newX *= (-1);
            }
            if (MathF.Abs(newY) >= 6.5f)
            {
                newY *= (-1);
            }

            EntityPool.AsteroidEntitiesPool.Find(e => e.Name.Contains(asteroidName)).CurrentX = newX;
            EntityPool.AsteroidEntitiesPool.Find(e => e.Name.Contains(asteroidName)).CurrentY = newY;
        
        TransformAsteroid?.Invoke(index, newX, newY);
    }

    public void Rotate(int index)
    {
            currentAngle = EntityPool.AsteroidEntitiesPool.Find(e => e.Name.Contains(ConstStrings.ASTEROIDNAME + index.ToString())).RotationAngle;
            rotateLeft = EntityPool.AsteroidEntitiesPool.Find(e => e.Name.Contains(ConstStrings.ASTEROIDNAME + index.ToString())).RotateLeft;

            angleDelta = (rotateLeft) ? rotationSpeed : -rotationSpeed;
            newAngle = currentAngle + angleDelta;
            EntityPool.AsteroidEntitiesPool.Find(e => e.Name.Contains(ConstStrings.ASTEROIDNAME + index.ToString())).RotationAngle = newAngle;

        RotateAsteroid?.Invoke(index, newAngle);
    }
}
