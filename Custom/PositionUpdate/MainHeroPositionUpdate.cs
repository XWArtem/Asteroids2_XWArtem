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
        var tempEntity = EntityPool.MainHero;
        float[] values = new float[3];
        values[0] = tempEntity.CurrentX;
        values[1] = tempEntity.CurrentY;
        values[2] = tempEntity.RotationAngle;
        return values;
    }
    public void Move(float moveForce)
    {
        var floatValues = GetValueOfEntity(ConstStrings.MAINHERONAME);

        currentX = floatValues[0];
        currentY = floatValues[1];
        angle = floatValues[2];

        deltaX = (-1) * moveSpeed * (float)Math.Sin((Math.PI / 180) * angle) * moveForce;
        deltaY = moveSpeed * (float)Math.Cos((Math.PI / 180) * angle) * moveForce;
        
        if (MathF.Abs(currentX + deltaX) >= 12.2f)
        {
            currentX *= (-1);
        }
        if (MathF.Abs(currentY + deltaY) >= 5.2f)
        {
            currentY *= (-1);
        }

        EntityPool.MainHero.CurrentX = currentX + deltaX;
        EntityPool.MainHero.CurrentY = currentY + deltaY;

        TransformMainHeroAction?.Invoke(currentX + deltaX, currentY + deltaY);
    }
    public void Rotate(bool rotateLeft)
    {
        var floatValues = GetValueOfEntity(ConstStrings.MAINHERONAME);
        currentRotationAngle = floatValues[2];

        if (rotateLeft)
        {
            RotateMainHeroAction?.Invoke(currentRotationAngle + deltaRotation * GameConfig.MainHeroRotateForce);
            EntityPool.MainHero.RotationAngle = currentRotationAngle + deltaRotation * GameConfig.MainHeroRotateForce;
        }
        else
        {
            RotateMainHeroAction?.Invoke(currentRotationAngle - deltaRotation * GameConfig.MainHeroRotateForce);
            EntityPool.MainHero.RotationAngle = currentRotationAngle - deltaRotation * GameConfig.MainHeroRotateForce;
        }
    }
}
