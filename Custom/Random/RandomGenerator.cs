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
            //if (Math.Abs(_pair[1].X) - Math.Abs(_pair[0].X) <= 2.0f)
            //{
            //    _pair[1].X -= 3.0f;
            //}
            //if (Math.Abs(_pair[1].Y) - Math.Abs(_pair[0].Y) <= 1.0f)
            //{
            //    _pair[0].Y -= 1.5f;
            //}
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
            return (float)_rand.NextDouble() * 11.6f * (_rand.Next(0, 1) * 2 - 1);
        }
        else
        {
            return (BoolRandom() == false) ? 11.6f : -11.6f;
        }
    }
    private float GetRandomY(bool getExtremeValue)
    {
        if (!getExtremeValue)
        {
            return (float)_rand.NextDouble() * 6.0f * (_rand.Next(0, 2) * 2 - 1);
        }
        else
        {
            return (BoolRandom() == false) ? 6.0f : -6.0f;
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