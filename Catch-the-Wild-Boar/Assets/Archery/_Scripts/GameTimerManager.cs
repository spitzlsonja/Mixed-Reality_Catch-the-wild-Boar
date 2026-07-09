using UnityEngine;
using TMPro;

public class GameTimerManager : MonoBehaviour
{
    [Header("Timer")]
    public float gameDuration = 120f;
    public TMP_Text timerText;

    [Header("End Board")]
    public GameObject endBoard;
    public TMP_Text endScoreText;

    [Header("Score Anzeige")]
    public TMP_Text currentScoreText;

    [Header("Game Objects")]
    public GameObject targets;
    public GameObject animals;

    [Header("Game Start Manager")]
    public GameStartManager gameStartManager;

    private float timeLeft;
    private bool timerRunning = false;

    private void Start()
    {
        timeLeft = gameDuration;
        UpdateTimerText();

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        if (endBoard != null)
            endBoard.SetActive(false);
    }

    private void Update()
    {
        if (!timerRunning)
            return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            EndGame();
        }

        UpdateTimerText();
    }

    public void StartTimer()
    {
        timeLeft = gameDuration;
        timerRunning = true;

        if (timerText != null)
            timerText.gameObject.SetActive(true);

        if (endBoard != null)
            endBoard.SetActive(false);

        if (targets != null)
            targets.SetActive(true);

        if (animals != null)
            animals.SetActive(true);

        UpdateTimerText();
    }

    private void EndGame()
    {
        timerRunning = false;

        if (timerText != null)
            timerText.gameObject.SetActive(false);

        if (targets != null)
            targets.SetActive(false);

        if (animals != null)
            animals.SetActive(false);

        if (endBoard != null)
            endBoard.SetActive(true);

        if (endScoreText != null)
        {
            if (currentScoreText != null)
            {
                endScoreText.text = "Deine Zeit ist abgelaufen!\n\n" + currentScoreText.text + "\n\nUm das Spiel neu zu Starten schieße auf Restart.\n";
            }
            else
            {
                endScoreText.text = "Zeit vorbei!";
            }
        }
        if (gameStartManager != null)
            gameStartManager.SetGameFinished();

    }

    private void UpdateTimerText()
    {
        if (timerText == null)
            return;

        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}