using System;

public class AsteroidsPositionUpdate 
{
    public static Action<float[], float[]> TransformAsteroid;
    public static Action<float[]> RotateAsteroid;
    private RandomGenerator _randomGenerator = new RandomGenerator();

    private float angle;
    private bool rotateLeft;

    private float currentAngle;
    private float angleDelta;

    private float currentX;
    private float currentY;
    private float startX;
    private float startY;
    private float destinationX;
    private float destinationY;

    private float deltaX;
    private float deltaY;

    private string asteroidName;
    private float[] newXarr = new float[SceneConfig.NumberOfAsteroids];
    private float[] newYarr = new float[SceneConfig.NumberOfAsteroids];
    private float[] newAngle = new float[SceneConfig.NumberOfAsteroids];


    private float rotationSpeed = 0.01f;
    private float moveSpeed = 0.0004f;

    public float[] GetEntityValues(string entityName)
    {
        var tempEntity = ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(entityName));
        float[] values = new float[7];
        values[0] = tempEntity.CurrentX;
        values[1] = tempEntity.CurrentY;
        values[2] = tempEntity.StartX;
        values[3] = tempEntity.StartY;
        values[4] = tempEntity.DestinationX;
        values[5] = tempEntity.DestinationY;
        values[6] = tempEntity.RotationAngle;
        return values;
    }

    public void Move()
    {
        for (int i = 0; i < SceneConfig.NumberOfAsteroids; i++)
        {
            var floatValues = GetEntityValues(ConstStrings.AsteroidName + i);
            currentX = floatValues[0];
            currentY = floatValues[1];
            startX = floatValues[2];
            startY = floatValues[3];
            destinationX = floatValues[4];
            destinationY = floatValues[5];

            asteroidName = ObjectEntityRepository.AllObjectsEntities.Find
                (e => e.Name.Contains(ConstStrings.AsteroidName + i.ToString())).Name;

            deltaX = (float)Math.Clamp((startX - destinationX) * moveSpeed, -0.005, +0.005);
            deltaY = (float)Math.Clamp((startY - destinationY) * moveSpeed, -0.005, +0.005);

            newXarr[i] = currentX + deltaX;
            newYarr[i] = currentY + deltaY;

            if (MathF.Abs( newXarr[i]) >= 12.2f)
            {
                newXarr[i] *= (-1);
            }
            if (MathF.Abs(newYarr[i]) >= 6.5f)
            {
                newYarr[i] *= (-1);
            }

            ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(asteroidName)).CurrentX = newXarr[i];
            ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(asteroidName)).CurrentY = newYarr[i];
        }
        TransformAsteroid?.Invoke(newXarr, newYarr);
    }

    public void Rotate()
    {
        for (int i = 0; i < SceneConfig.NumberOfAsteroids; i++)
        {
            currentAngle = ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(ConstStrings.AsteroidName + i.ToString())).RotationAngle;
            rotateLeft = ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(ConstStrings.AsteroidName + i.ToString())).RotateLeft;

            angleDelta = (rotateLeft) ? rotationSpeed : -rotationSpeed;
            newAngle[i] = currentAngle + angleDelta;
            ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(ConstStrings.AsteroidName + i.ToString())).RotationAngle = newAngle[i];
        }
        RotateAsteroid?.Invoke(newAngle);
    }
}
