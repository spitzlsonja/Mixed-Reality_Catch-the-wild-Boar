using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [Header("Intro UI")]
    public GameObject introBoard;

    [Header("Game Objects")]
    public GameObject bow;
    public GameObject targets;
    public GameObject animals;

    [Header("Timer")]
    public GameTimerManager gameTimerManager;

    private bool gameStarted = false;

    private void Start()
    {
        gameStarted = false;

        if (introBoard != null)
            introBoard.SetActive(true);
    }

    public void StartGame()
    {
        if (gameStarted)
            return;

        gameStarted = true;

        Debug.Log("StartGame wurde ausgeführt!");

        if (introBoard != null)
            introBoard.SetActive(false);

        if (targets != null)
            targets.SetActive(true);

        if (animals != null)
            animals.SetActive(true);

        if (gameTimerManager != null)
        {
            gameTimerManager.StartTimer();
        }
        else
        {
            Debug.LogWarning("GameTimerManager ist im GameStartManager NICHT eingetragen!");
        }
    }
}