using System;
using System.Threading.Tasks;

public class MainHeroWeapon
{
    public static Action<float, float, float, int> bulletSpawnAction;

    private float bulletStartX;
    private float bulletStartY;
    private float bulletAngle;
    private int bulletIndex;
    private bool _firstShootOnCooldown;

    public void PerfomBulletShoot()
    {
        if (!_firstShootOnCooldown)
        {
            bulletStartX = ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(ConstStrings.MainHeroName)).CurrentX;
            bulletStartY = ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(ConstStrings.MainHeroName)).CurrentY;
            bulletAngle = ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(ConstStrings.MainHeroName)).RotationAngle;
            bulletSpawnAction?.Invoke(bulletStartX, bulletStartY, bulletAngle, bulletIndex);
            bulletIndex++;
            _firstShootOnCooldown = true;
            FirstShootCooldown();
        }
    }

    private async void FirstShootCooldown()
    {
        await Task.Delay(500);
        _firstShootOnCooldown = false;
    }
}
