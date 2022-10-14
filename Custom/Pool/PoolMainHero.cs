using UnityEngine;

class PoolMainHero : PoolBase
{
    public GameObject MainHero;

    public PoolMainHero()
    {
        FillPool(1);
    }

    public override void FillPool(int capacity)
    {
        ObjectEntity mainHeroEntity = new MainHeroEntity
            (ConstStrings.MAINHERONAME,
            0,
            GameConfig.MainHeroSpawnPositionCoordinateX,
            GameConfig.MainHeroSpawnPositionCoordinateY,
            0f,
            ConstStrings.MAINHEROSPRITEPATH,
            EntityType.mainHero);

        CreateElement(mainHeroEntity);

        EntityPool.MainHero = mainHeroEntity;
        MainHero = tempGameObject;
    }

    public override bool TryGetElement(out GameObject element, int index)
    {
        element = MainHero;
        return true;
    }
}

