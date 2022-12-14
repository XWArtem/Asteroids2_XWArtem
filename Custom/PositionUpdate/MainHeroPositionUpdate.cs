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
        var tempEntity = PoolEntity.MainHero;
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
        
        if (MathF.Abs(currentX + deltaX) >= GameConfig.MaxAxisX)
        {
            currentX *= (-1);
        }
        if (MathF.Abs(currentY + deltaY) >= GameConfig.MaxAxisY + GameConfig.MainHeroRadius)
        {
            currentY *= (-1);
        }

        PoolEntity.MainHero.CurrentX = currentX + deltaX;
        PoolEntity.MainHero.CurrentY = currentY + deltaY;

        TransformMainHeroAction?.Invoke(currentX + deltaX, currentY + deltaY);
    }
    public void Rotate(bool rotateLeft)
    {
        var floatValues = GetValueOfEntity(ConstStrings.MAINHERONAME);
        currentRotationAngle = floatValues[2];

        if (rotateLeft)
        {
            RotateMainHeroAction?.Invoke(currentRotationAngle + deltaRotation * GameConfig.MainHeroRotateForce);
            PoolEntity.MainHero.RotationAngle = currentRotationAngle + deltaRotation * GameConfig.MainHeroRotateForce;
        }
        else
        {
            RotateMainHeroAction?.Invoke(currentRotationAngle - deltaRotation * GameConfig.MainHeroRotateForce);
            PoolEntity.MainHero.RotationAngle = currentRotationAngle - deltaRotation * GameConfig.MainHeroRotateForce;
        }
    }
}
