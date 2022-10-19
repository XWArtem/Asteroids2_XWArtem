using System;
using UnityEngine;

public class BulletPositionUpdate
{
    public static Action<int, float, float, bool> TransformBullet;

    private float newX;
    private float newY;
    private float currentX;
    private float currentY;
    private float rotationAngle;
    private const float bulletMoveSpeed = GameConfig.BulletMoveSpeed;
    private float deltaX;
    private float deltaY;
    private bool isOutOfRange;

    public void Move(int bulletIndex)
    {
        var entity = PoolEntity.BulletEntitiesPool.Find
            (e => e.Name.Contains(ConstStrings.BULLETNAME + bulletIndex.ToString()));

        currentX = entity.CurrentX;
        currentY = entity.CurrentY;
        rotationAngle = entity.RotationAngle;

        deltaX = (-1) * bulletMoveSpeed * (float)Math.Sin((Math.PI / 180) * rotationAngle);
        deltaY = bulletMoveSpeed * (float)Math.Cos((Math.PI / 180) * rotationAngle);

        newX = currentX + deltaX;
        newY = currentY + deltaY;

        PoolEntity.BulletEntitiesPool.Find
            (e => e.Name.Contains
            (ConstStrings.BULLETNAME + bulletIndex.ToString())).CurrentX = newX;

        PoolEntity.BulletEntitiesPool.Find
            (e => e.Name.Contains
            (ConstStrings.BULLETNAME + bulletIndex.ToString())).CurrentY = newY;

        if (Mathf.Abs(newX) > 12.0f || Mathf.Abs(newY) > 5.0f)
        {
            isOutOfRange = true;
        }
        else
        {
            isOutOfRange = false;
        }
        TransformBullet?.Invoke(bulletIndex, newX, newY, isOutOfRange);
    }
}
