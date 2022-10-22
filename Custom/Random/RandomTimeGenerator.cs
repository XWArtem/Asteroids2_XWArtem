using System;

public class RandomTimeGenerator
{
    private Random _rand = new Random();

    public int GetRandomTimerForSpawn()
    {
        return _rand.Next(GameConfig.MinUFORespawnTimer, GameConfig.MaxUFORespawnTimer);
    }
}