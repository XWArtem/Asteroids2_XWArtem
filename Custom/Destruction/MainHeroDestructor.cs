public class MainHeroDestructor : ObjectDestructorBase
{
    public override void Destroy(ObjectEntity mainHeroEntity)
    {
        var itemToRemove = ObjectEntityRepository.AllObjectsEntities.Find(e => e.Name.Contains(ConstStrings.MainHeroName));
        ObjectEntityRepository.AllObjectsEntities.Remove(itemToRemove);
        UnityEngine.Object.Destroy(UnityEngineUpdate.Instance.MainHero);
    }
}
