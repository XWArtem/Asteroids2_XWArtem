using System;
using System.Threading.Tasks;
using UnityEngine;

public class BulletPositionUpdate
{
    public static Action<float[], float[]> TransformBullet;
    private BulletDestructor _bulletDestructor = new BulletDestructor();

    private float[] newXarr;
    private float[] newYarr;
    private float currentX;
    private float currentY;
    private float rotationAngle;
    private const float bulletMoveSpeed = 0.05f;
    private float deltaX;
    private float deltaY;

    public void Move(int bulletsAmount)
    {
        if (bulletsAmount == 0)
        {
            return;
        }

        newXarr = new float[bulletsAmount];
        newYarr = new float[bulletsAmount];

        for (int i = 0; i < bulletsAmount; i++)
        {

            int index = i;
            
            currentX = ObjectEntityRepository.AllObjectsEntities.Find
                (e => e.Name.Contains(ConstStrings.BulletName + index.ToString())).CurrentX;

            currentY = ObjectEntityRepository.AllObjectsEntities.Find
                (e => e.Name.Contains(ConstStrings.BulletName + index.ToString())).CurrentY;

            rotationAngle = ObjectEntityRepository.AllObjectsEntities.Find
                (e => e.Name.Contains(ConstStrings.BulletName + index.ToString())).RotationAngle;

            deltaX = (-1) * bulletMoveSpeed * (float)Math.Sin((Math.PI / 180) * rotationAngle);
            deltaY = bulletMoveSpeed * (float)Math.Cos((Math.PI / 180) * rotationAngle);

            newXarr[index] = currentX + deltaX;
            newYarr[index] = currentY + deltaY;

            ObjectEntityRepository.AllObjectsEntities.Find
                (e => e.Name.Contains(ConstStrings.BulletName + index.ToString())).CurrentX = newXarr[index];

            ObjectEntityRepository.AllObjectsEntities.Find
                (e => e.Name.Contains(ConstStrings.BulletName + index.ToString())).CurrentY = newYarr[index];
        }
        TransformBullet?.Invoke(newXarr, newYarr);

        foreach (var X in newXarr)
        {
            int index = Array.IndexOf(newXarr, X);
            if (Mathf.Abs(newXarr[index]) > 12.0f)
            {
                DestroyBullet(ObjectEntityRepository.AllObjectsEntities.Find
                    (e => e.Name.Contains(ConstStrings.BulletName + index.ToString())), index);
            }
        }
    }

    private void DestroyBullet(ObjectEntity objectEntity, int bulletIndex)
    {
        _bulletDestructor.Destroy(objectEntity, bulletIndex);
    }
}
