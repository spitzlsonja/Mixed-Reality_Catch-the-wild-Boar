using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int shots = 0;
    private int hits = 0;

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddShot()
    {
        shots++;
        UpdateScoreText();
    }

    public void AddHit()
    {
        hits++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Schüsse: " + shots + "\nTreffer: " + hits;
    }
}