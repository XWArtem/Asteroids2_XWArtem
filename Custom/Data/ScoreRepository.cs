public class ScoreRepository
{
    public delegate void ScoreChanged(int score);
    public static event ScoreChanged OnScoreChanged;

    private static int _maxScore;
    public static int MaxScore
    {
        get
        {
            return _maxScore;
        }
        set
        {
            if (value > 0 && value > _maxScore)
            {
                _maxScore = value;
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
            if (value > 0)
            {
                _currentScore = value;
                OnScoreChanged(_currentScore);
            }
        }
    }
}
