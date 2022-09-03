using System.Collections.Generic;

public class ObjectEntityRepository
{
    public static List <ObjectEntity> AllObjectsEntities = new List<ObjectEntity>();

    public static void CreateObjectEntity(ObjectEntity objectEntity)
    {
        AllObjectsEntities.Add(objectEntity);
    }
}
