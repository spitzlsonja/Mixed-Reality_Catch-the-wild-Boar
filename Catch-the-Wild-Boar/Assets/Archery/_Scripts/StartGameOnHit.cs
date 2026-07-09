using UnityEngine;

public class StartGameOnHit : MonoBehaviour
{
    public GameStartManager gameStartManager;

    private bool alreadyStarted = false;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("StartHitBox getroffen von: " + collision.gameObject.name);

        if (alreadyStarted)
            return;

        alreadyStarted = true;

        if (gameStartManager != null)
        {
            Debug.Log("Spiel startet!");
            gameStartManager.StartGame();
        }
        else
        {
            Debug.LogWarning("GameStartManager fehlt!");
        }
    }
}