using UnityEngine;

public class StartGameOnHit : MonoBehaviour
{
    public GameStartManager gameStartManager;

    private bool alreadyStarted = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (alreadyStarted)
            return;

        // Prüft, ob wirklich ein Pfeil getroffen hat
        if (collision.gameObject.GetComponent<StickingArrowToSurface>() != null)
        {
            alreadyStarted = true;

            if (gameStartManager != null)
            {
                gameStartManager.StartGame();
            }
        }
    }
}