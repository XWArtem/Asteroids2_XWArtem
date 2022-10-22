using System;

public struct PairOfFloats
{
    public float X;
    public float Y;
}

public class RandomGenerator
{
    private Random _rand = new Random();
    PairOfFloats _pair = new PairOfFloats();
    public PairOfFloats[] RandomPosRoute
    {
        get
        {
            PairOfFloats[] _pair = new PairOfFloats[2];
            bool randomSideSelection = (_rand.Next(1, 3) % 2 == 0);
            _pair[0].X = GetRandomX(randomSideSelection);
            _pair[0].Y = GetRandomY(!randomSideSelection);
            _pair[1].X = GetRandomX(!randomSideSelection);
            _pair[1].Y = GetRandomY(randomSideSelection);
            return _pair;
        }
        private set
        {
            RandomPosRoute = value;
        }
    }
    public PairOfFloats RandomPos
    {
        get
        {
            bool randomSideSelection = (_rand.Next(1, 3) % 2 == 0);
            _pair.X = GetRandomX(randomSideSelection);
            _pair.Y = GetRandomY(!randomSideSelection);
            return _pair;
        }
        private set
        {
            RandomPos = value;
        }
    }
    private float GetRandomX(bool getExtremeValue)
    {
        if (!getExtremeValue)
        {
            return (float)_rand.NextDouble() * GameConfig.MaxAxisX * (_rand.Next(0, 1) * 2 - 1);
        }
        else
        {
            return (BoolRandom() == false) ? GameConfig.MaxAxisX + 2.0f : -GameConfig.MaxAxisX - 2.0f;
        }
    }
    private float GetRandomY(bool getExtremeValue)
    {
        if (!getExtremeValue)
        {
            return (float)_rand.NextDouble() * GameConfig.MaxAxisY * (_rand.Next(0, 2) * 2 - 1);
        }
        else
        {
            return (BoolRandom() == false) ? GameConfig.MaxAxisY + 1.0f : -GameConfig.MaxAxisY - 1.0f;
        }
    }

    public float GetRandomSpeed(EntityType entityType)
    {
        if (entityType == EntityType.asteroid)
        {
            return (float)0.001 * _rand.Next(4, 39); // result asteroid speed: 0.004 - 0.038
        }
        else if (entityType == EntityType.smallAsteroid)
        {
            return (float)0.01 * _rand.Next(4, 9); // result asteroid speed: 0.04 - 0.09
        }
        else if (entityType == EntityType.UFO)
        {
            return (float)0.001 * _rand.Next(8, 21); // result UFO speed: 0.004 - 0.01
        }
        else 
        {
            return 0f;
        }
    }

    public float RotationZ
    {
        get
        {
            return (float)_rand.NextDouble() * 180.0f;
        }
        private set
        {
           RotationZ = value;
        }
    }
    public bool RotateLeftRandom
    {
        get
        {
            return BoolRandom();
        }
        private set
        {
            RotateLeftRandom = value;
        }
    }
    public bool BoolRandom()
    {
        return (_rand.Next(1, 3) % 2 == 0);
    }
}