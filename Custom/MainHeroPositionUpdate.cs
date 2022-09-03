using System;

public class MainHeroPositionUpdate : ITransformable
{
    public static Action<float, float> TransformMainHeroAction;
    public static Action<float> RotateMainHeroAction;

    private float angle = 0.0f;

    private float currentX;
    private float currentY;
    private float deltaX;
    private float deltaY;
    private float deltaRotation = 0.3f;
    private float moveSpeed = 0.01f;

    private float currentRotationAngle;
    public float[] GetValueOfEntity(string entityName)
    {
        var tempEntity = ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(entityName));
        float[] values = new float[3];
        values[0] = tempEntity.CurrentX;
        values[1] = tempEntity.CurrentY;
        values[2] = tempEntity.RotationAngle;
        return values;
    }
    public void Move(float moveForce)
    {
        var floatValues = GetValueOfEntity(ConstStrings.MainHeroName);

        currentX = floatValues[0];
        currentY = floatValues[1];
        angle = floatValues[2];

        deltaX = (-1) * moveSpeed * (float)Math.Sin((Math.PI / 180) * angle) * moveForce;
        deltaY = moveSpeed * (float)Math.Cos((Math.PI / 180) * angle) * moveForce;

        TransformMainHeroAction?.Invoke(currentX + deltaX, currentY + deltaY);

        ObjectEntityRepository.AllObjectsEntities.Find
            (e => e.Name.Contains(ConstStrings.MainHeroName)).CurrentX = currentX + deltaX;

        ObjectEntityRepository.AllObjectsEntities.Find
            (e => e.Name.Contains(ConstStrings.MainHeroName)).CurrentY = currentY + deltaY;
    }
    public void Rotate(bool rotateLeft)
    {
        var floatValues = GetValueOfEntity(ConstStrings.MainHeroName);
        currentRotationAngle = floatValues[2];

        if (rotateLeft)
        {
            RotateMainHeroAction?.Invoke(currentRotationAngle + deltaRotation);
            ObjectEntityRepository.AllObjectsEntities.Find
        (e => e.Name.Contains(ConstStrings.MainHeroName)).RotationAngle = currentRotationAngle + deltaRotation;
        }
        else
        {
            RotateMainHeroAction?.Invoke(currentRotationAngle - deltaRotation);
            ObjectEntityRepository.AllObjectsEntities.Find
        (e => e.Name.Contains(ConstStrings.MainHeroName)).RotationAngle = currentRotationAngle - deltaRotation;
        }
    }
    public void BulletMove()
    {

    }
}
