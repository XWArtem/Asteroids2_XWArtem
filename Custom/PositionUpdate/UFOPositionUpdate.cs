using System;
using System.Collections.Generic;

class UFOPositionUpdate
{
    public static Action<List<ObjectEntity>, int, float, float> TransformUFOAction;

    private float currentX;
    private float currentY;
    private float destinationX;
    private float destinationY;

    private float directionX;
    private float directionY;

    private float deltaX;
    private float deltaY;

    private float newX;
    private float newY;

    private float speedRatio;

    public void TransformUFO(int index)
    {
        Move(PoolEntity.UFOEntitiesPool, index, ConstStrings.UFONAME);
    }

    public float[] GetEntityValues(List<ObjectEntity> poolEntityList, string entityName)
    {
        var tempEntity = poolEntityList.Find(e => e.Name.Contains(entityName));
        float[] values = new float[4];
        values[0] = tempEntity.CurrentX;
        values[1] = tempEntity.CurrentY;
        values[2] = PoolEntity.MainHero.CurrentX;
        values[3] = PoolEntity.MainHero.CurrentY;
        return values;
    }

    private void Move(List<ObjectEntity> poolEntityList, int index, string UFOName)
    {
        var floatValues = GetEntityValues(poolEntityList, UFOName + index);
        currentX = floatValues[0];
        currentY = floatValues[1];
        destinationX = floatValues[2];
        destinationY = floatValues[3];

        UFOName += index.ToString();

        directionX = (float)(destinationX - currentX);
        directionY = (float)(destinationY - currentY);

        if (MathF.Abs(directionX) <= MathF.Abs(directionY))
        {
            speedRatio = MathF.Abs(directionX / directionY);
            deltaX = (float)Math.Clamp(directionX, -0.05f, 0.05f) * speedRatio * GameConfig.UFOMoveSpeed;
            deltaY = (float)Math.Clamp(directionY, -0.05f, 0.05f) * (1 - speedRatio) * GameConfig.UFOMoveSpeed;
        }
        else
        {
            speedRatio = MathF.Abs(directionY / directionX);
            deltaX = (float)Math.Clamp(directionX, -0.05f, 0.05f) * (1 - speedRatio) * GameConfig.UFOMoveSpeed;
            deltaY = (float)Math.Clamp(directionY, -0.05f, 0.05f) * speedRatio * GameConfig.UFOMoveSpeed;
        }

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

        poolEntityList.Find(e => e.Name.Contains(UFOName)).CurrentX = newX;
        poolEntityList.Find(e => e.Name.Contains(UFOName)).CurrentY = newY;

        TransformUFOAction?.Invoke(poolEntityList, index, newX, newY);
    }
}