using System.Collections.Generic;

public class EntityPool
{
    public static List<ObjectEntity> AsteroidEntitiesPool = new List<ObjectEntity>();
    public static List<ObjectEntity> BulletEntitiesPool = new List<ObjectEntity>();
    public static List<ObjectEntity> UfoEntitiesPool = new List<ObjectEntity>();
    public static ObjectEntity MainHero;
}
