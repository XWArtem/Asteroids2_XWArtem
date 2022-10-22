using UnityEngine;

public class RepositoryBase
{
    private const string KEY_MAXSCORE = "KEY_MAXSCORE";
    public int MaxScore = 0;

    public RepositoryBase() 
    {
        MaxScore = PlayerPrefs.GetInt(KEY_MAXSCORE, 0);
    }

    public void Save() 
    {
        PlayerPrefs.SetInt(KEY_MAXSCORE, ScoreRepository.MaxScore);
    }
}