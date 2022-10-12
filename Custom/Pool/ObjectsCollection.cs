public enum EntityType
{
    mainHero,
    asteroid,
    UFO,
    bullet
}

public abstract class ObjectEntity
{
    public string Name;
    public int Index;
    public float CurrentX;
    public float CurrentY;
    public float StartX = 0.0f;
    public float StartY = 0.0f;
    public float DestinationX = 0.0f;
    public float DestinationY = 0.0f;
    public float RotationAngle;
    public bool RotateLeft;
    public string ImagePath;
    public bool IsMainHero;
    public EntityType _entityType;

    public ObjectEntity(string name,
        int index,
        float X, float Y, 
        float rotationAngle, 
        string imagePath, 
        EntityType entityType)
    {
        Name = name;
        Index = index;
        CurrentX = X;
        CurrentY = Y;
        RotationAngle = rotationAngle;
        ImagePath = imagePath;
        _entityType = entityType;
    }
    public ObjectEntity(string name, 
        float X, float Y, 
        float startX, float startY, 
        float destinationX, float destinationY, 
        float rotationAngle, 
        bool rotateLeft,
        string imagePath, 
        EntityType entityType)
    {
        Name = name;
        CurrentX = X;
        CurrentY = Y;
        StartX = startX;
        StartY = startY;
        DestinationX = destinationX;
        DestinationY = destinationY;
        RotationAngle = rotationAngle;
        RotateLeft = rotateLeft;
        ImagePath = imagePath;
        _entityType = entityType;
    }
}

public class MainHeroEntity : ObjectEntity
{
    public MainHeroEntity
        (string name, int index, float X, float Y, float rotationAngle, string imagePath, EntityType entityType) 
        : base(name, index, X, Y, rotationAngle, imagePath, EntityType.mainHero) { }
}

public class AsteroidEntity : ObjectEntity
{
    public AsteroidEntity
        (string name, float X, float Y, float startX, float startY, float destinationX, float destinationY, float rotationAngle, bool rotateLeft, string imagePath, EntityType entityType) 
        : base(name, X, Y, startX, startY, destinationX, destinationY, rotationAngle, rotateLeft, imagePath, EntityType.asteroid) { }
}

public class BulletEntity : ObjectEntity
{
    public BulletEntity(string name, int index, float X, float Y, float rotationAngle, string imagePath, EntityType entityType)
        : base(name, index, X, Y, rotationAngle, imagePath, EntityType.bullet) { }
}

public class UfoEntity : ObjectEntity
{
    public UfoEntity(string name, float X, float Y, float startX, float startY, float destinationX, float destinationY, float rotationAngle, bool rotateLeft, string imagePath, EntityType entityType)
        : base(name, X, Y, startX, startY, destinationX, destinationY, rotationAngle, rotateLeft, imagePath, EntityType.UFO) { }
}