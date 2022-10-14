using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        ScoreRepository.OnScoreChanged += ChangeScoreText;
    }
    private void OnDisable()
    {
        ScoreRepository.OnScoreChanged -= ChangeScoreText;
    }

    private void ChangeScoreText(int score)
    {
        _scoreText.text = score.ToString();
    }
}
