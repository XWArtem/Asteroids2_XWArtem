public class BulletDestructor : ObjectDestructorBase
{
    public override void Destroy(ObjectEntity bulletEntity, int bulletIndex)
    {
        var itemToRemove = ObjectEntityRepository.AllObjectsEntities.Find
            (e => e.Name.Contains(ConstStrings.BulletName + bulletIndex.ToString()));

        ObjectEntityRepository.AllObjectsEntities.Remove(itemToRemove);

        UnityEngine.Object.Destroy(UnityEngineUpdate.Instance.Bullets[bulletIndex]);
        UnityEngineUpdate.Instance.Bullets.Remove(UnityEngineUpdate.Instance.Bullets[bulletIndex]);

        
        
    }
}
