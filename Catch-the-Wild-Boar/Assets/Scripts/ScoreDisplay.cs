using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoreText;

    private int points = 0;
    private int arrows = 10;

    void Start()
    {
        UpdateDisplay();
    }

    public void AddPoint()
    {
        points++;
        UpdateDisplay();
    }

    public void AddFivePoints()
    {
        points += 5;
        UpdateDisplay();
    }

    public void UseArrow()
    {
        if (arrows > 0)
        {
            arrows--;
            UpdateDisplay();
        }
    }

    public void AddArrow()
    {
        arrows++;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        scoreText.text = "Punkte: " + points + "\nPfeile: " + arrows;
    }
}