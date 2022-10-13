using System;

// returns true, if collision was detected

public static class CollisionDetection
{
    public static bool CheckCollision(float firstObjectX, float firstObjectY, float secondObjectX, float secondObjectY, float radius)
    {
        return (MathF.Sqrt(MathF.Pow((secondObjectX - firstObjectX), 2f) 
            + MathF.Pow((secondObjectY - firstObjectY), 2))) <= radius;
    }
}
