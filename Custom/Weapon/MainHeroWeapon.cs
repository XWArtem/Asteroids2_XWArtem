using System;
using System.Threading.Tasks;

public class MainHeroWeapon
{
    public static Action<float, float, float, int> bulletSpawnAction;
    public static Action FirstShootPerfomed;

    private float bulletStartX;
    private float bulletStartY;
    private float bulletAngle;
    private int bulletIndex;
    private bool _firstShootOnCooldown;

    public void PerfomBulletShoot()
    {
        if (!_firstShootOnCooldown)
        {
            bulletStartX = PoolEntity.MainHero.CurrentX;
            bulletStartY = PoolEntity.MainHero.CurrentY;
            bulletAngle = PoolEntity.MainHero.RotationAngle;

            PoolEntity.BulletEntitiesPool.Find
                (e => e.Name.Contains(ConstStrings.BULLETNAME + bulletIndex.ToString())).CurrentX = bulletStartX;
            PoolEntity.BulletEntitiesPool.Find
                (e => e.Name.Contains(ConstStrings.BULLETNAME + bulletIndex.ToString())).CurrentY = bulletStartY;
            
            bulletSpawnAction?.Invoke(bulletStartX, bulletStartY, bulletAngle, bulletIndex);
            bulletIndex = (++bulletIndex < GameConfig.NumberOfBullets) ? bulletIndex++ : 0;
            _firstShootOnCooldown = true;
            FirstShootCooldown();
            FirstShootPerfomed?.Invoke();
        }
    }

    private async void FirstShootCooldown()
    {
        await Task.Delay(500);
        _firstShootOnCooldown = false;
    }
}
