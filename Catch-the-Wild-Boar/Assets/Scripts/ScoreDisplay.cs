using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text startText;
    public TMP_Text hudText;

    public GameObject startBoard;

    private int points = 0;
    private int arrows = 10;

    void Start()
    {
        UpdateDisplay();

        if (startBoard != null)
        {
            Invoke(nameof(HideStartBoard), 4f);
        }
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
        string text = "Punkte: " + points + "\nPfeile: " + arrows;

        if (startText != null)
        {
            startText.text = text;
        }

        if (hudText != null)
        {
            hudText.text = text;
        }
    }

    private void HideStartBoard()
    {
        startBoard.SetActive(false);
    }
}