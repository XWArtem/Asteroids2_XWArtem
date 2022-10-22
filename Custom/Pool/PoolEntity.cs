using System.Collections.Generic;

public class PoolEntity
{
    public static List<ObjectEntity> AsteroidEntitiesPool = new List<ObjectEntity>();
    public static List<ObjectEntity> SmallAsteroidEntitiesPool = new List<ObjectEntity>();
    public static List<ObjectEntity> BulletEntitiesPool = new List<ObjectEntity>();
    public static List<ObjectEntity> UFOEntitiesPool = new List<ObjectEntity>();
    public static ObjectEntity MainHero;
}
