using UnityEngine;

public class RepositoryBase
{
    private const string KEY_SCORE = "KEY_SCORE";
    public int MaxScore = 0;

    public RepositoryBase() 
    {
        MaxScore = PlayerPrefs.GetInt(KEY_SCORE, 0);
    }

    public void Save() 
    {
        PlayerPrefs.SetInt(KEY_SCORE, ScoreRepository.MaxScore);
    }
}