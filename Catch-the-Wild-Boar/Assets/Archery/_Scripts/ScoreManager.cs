using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Anzeige")]
    public TMP_Text scoreText;

    private int hits = 0;
    private int shots = 0;

    private void Start()
    {
        ResetScore();
    }

    public void AddHit()
    {
        hits++;
        UpdateScoreText();
    }

    public void AddShot()
    {
        shots++;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        hits = 0;
        shots = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Treffer: " + hits + "\nSchüsse: " + shots;
        }
    }
}