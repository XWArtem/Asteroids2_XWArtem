using UnityEngine;
using System.Collections.Generic;

class PoolBullet : PoolBase
{
    public static List<GameObject> _bulletList = new List<GameObject>();

    public PoolBullet(int capacity)
    {
        FillPool(capacity);
        MainHeroWeapon.bulletSpawnAction += GetFreeBullet;
    }

    public override void FillPool (int capacity)
    {
        for (int i = 0; i < capacity; i++)
        {
            int index = i;
            ObjectEntity bulletEntity = new BulletEntity
            (ConstStrings.BULLETNAME + index.ToString(),
            index,
            0f,
            0f,
            0f,
            ConstStrings.BULLETSPRITEPATH,
            EntityType.bullet);

            EntityPool.BulletEntitiesPool.Add(bulletEntity);
            base.tempGameObject = base.CreateElement(bulletEntity);
            base.tempGameObject.SetActive(false);
            _bulletList.Add(base.tempGameObject);
        }
    }

    private void GetFreeBullet(float startX, float startY, float angle, int index)
    {
        GetFreeElement(startX, startY, angle, index);
    }

    public override bool TryGetElement(out GameObject element, int bulletIndex)
    {
        foreach (var item in _bulletList)
        {
            if (!item.gameObject.activeInHierarchy 
                && item.name.Contains(ConstStrings.BULLETNAME + bulletIndex.ToString()))
            {
                element = item;
                item.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public static void ReturnToPool(int bulletIndex)
    {
        EntityPool.BulletEntitiesPool.Find
                (e => e.Name.Contains(ConstStrings.BULLETNAME + bulletIndex.ToString())).CurrentX = 0;
        EntityPool.BulletEntitiesPool.Find
            (e => e.Name.Contains(ConstStrings.BULLETNAME + bulletIndex.ToString())).CurrentY = 0;

        _bulletList[bulletIndex].SetActive(false);
    }
}