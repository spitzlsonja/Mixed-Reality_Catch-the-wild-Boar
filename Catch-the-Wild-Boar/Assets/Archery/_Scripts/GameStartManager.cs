using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [Header("Boards")]
    public GameObject introBoard;
    public GameObject endBoard;

    [Header("Game Objects")]
    public GameObject bow;
    public GameObject targets;
    public GameObject animals;

    [Header("Timer")]
    public GameTimerManager gameTimerManager;

    private bool gameRunning = false;


    private void Start()
    {
        gameRunning = false;

        if (introBoard != null)
            introBoard.SetActive(true);

        if (endBoard != null)
            endBoard.SetActive(false);

        if (targets != null)
            targets.SetActive(false);

        if (animals != null)
            animals.SetActive(false);
    }

    public bool IsGameRunning()
    {
        return gameRunning;
    }

    public void StartGame()
    {
        StartOrRestartGame();
    }

    public void RestartGame()
    {
        StartOrRestartGame();
    }

    private void StartOrRestartGame()
    {
        gameRunning = true;

        Debug.Log("Spiel startet / restartet!");

        if (introBoard != null)
            introBoard.SetActive(false);

        if (endBoard != null)
            endBoard.SetActive(false);

        if (targets != null)
            targets.SetActive(true);

        if (animals != null)
            animals.SetActive(true);

        // Score zurücksetzen
        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.ResetScore();
        }
        else
        {
            Debug.LogWarning("ScoreManager wurde nicht gefunden!");
        }

        if (gameTimerManager != null)
        {
            gameTimerManager.StartTimer();
        }
        else
        {
            Debug.LogWarning("GameTimerManager ist im GameStartManager NICHT eingetragen!");
        }
    }

    public void SetGameFinished()
    {
        gameRunning = false;
    }
}