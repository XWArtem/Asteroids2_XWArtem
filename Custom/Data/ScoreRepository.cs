public class ScoreRepository
{
    public delegate void ScoreChanged(int score);
    public static event ScoreChanged OnScoreChanged;

    private static RepositoryBase _repositoryBase = new RepositoryBase();

    private static int _maxScore = _repositoryBase.MaxScore;
    public static int MaxScore
    {
        get
        {
            return _maxScore;
        }
        set
        {
            if (value >= 0 && value > _maxScore)
            {
                _maxScore = value;
                _repositoryBase.Save();
            }
        }
    }

    private static int _currentScore;
    public static int CurrentScore
    {
        get
        {
            return _currentScore;
        }
        set
        {
            if (value >= 0)
            {
                _currentScore = value;
                MaxScore = _currentScore;
                OnScoreChanged(_currentScore);
            }
        }
    }

}
