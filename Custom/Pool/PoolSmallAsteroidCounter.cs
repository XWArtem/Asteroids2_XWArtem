using System.Collections.Generic;

class PoolSmallAsteroidCounter
{
    public List<int> _smallAsteroidsOnScene;

    public PoolSmallAsteroidCounter()
    {
        _smallAsteroidsOnScene = new List<int>();
    }

    // returns false if both indexes are missing (are disabled)
    public bool CheckSmallAsteroids(int index1)
    {
        if (_smallAsteroidsOnScene != null)
        {
            int index2 = (GameConfig.NumberOfSmallAsteroids - 1 - index1);
            return (_smallAsteroidsOnScene.Contains(index1) || _smallAsteroidsOnScene.Contains(index2));
        }
        else
        {
            return true;
        }
    }

}

