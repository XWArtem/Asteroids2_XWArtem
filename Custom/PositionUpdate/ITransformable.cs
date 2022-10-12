public interface ITransformable
{
    public void Move(float moveSpeed);
    public void Rotate(bool rotateLeft);
    public float[] GetValueOfEntity(string entityName);
}
