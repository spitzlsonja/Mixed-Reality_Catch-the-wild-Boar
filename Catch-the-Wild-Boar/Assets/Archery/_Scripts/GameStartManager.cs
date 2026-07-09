using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    [Header("Intro UI")]
    public GameObject introBoard;

    [Header("Game Objects")]
    public GameObject bow;
    public GameObject targets;
    public GameObject animals;

    private bool gameStarted = false;

    private void Start()
    {
        gameStarted = false;

        // Intro anzeigen
        if (introBoard != null)
            introBoard.SetActive(true);

        // Spielobjekte am Anfang deaktivieren
        //if (bow != null)
            //bow.SetActive(false);

        //if (targets != null)
            //targets.SetActive(false);

        //if (animals != null)
            //animals.SetActive(false);
    }

    public void StartGame()
    {
        gameStarted = true;

        // Intro ausblenden
        if (introBoard != null)
            introBoard.SetActive(false);

        // Spiel starten
       // if (bow != null)
           // bow.SetActive(true);

        if (targets != null)
            targets.SetActive(true);

        if (animals != null)
            animals.SetActive(true);
    }
}