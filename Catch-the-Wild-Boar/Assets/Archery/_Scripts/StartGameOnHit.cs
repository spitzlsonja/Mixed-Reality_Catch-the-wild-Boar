using UnityEngine;

public class StartGameOnHit : MonoBehaviour
{
    public GameStartManager gameStartManager;

    [Header("Ist das der Restart Button?")]
    public bool isRestartButton = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Arrow"))
            return;

        Debug.Log("Button getroffen von Pfeil: " + collision.gameObject.name);

        if (gameStartManager == null)
        {
            Debug.LogWarning("GameStartManager fehlt!");
            return;
        }

        if (isRestartButton)
        {
            gameStartManager.RestartGame();
        }
        else
        {
            gameStartManager.StartGame();
        }
    }
}