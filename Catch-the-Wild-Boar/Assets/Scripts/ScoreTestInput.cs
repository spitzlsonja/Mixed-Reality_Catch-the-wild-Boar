using UnityEngine;

public class ScoreTestInput : MonoBehaviour
{
    public ScoreManager scoreManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scoreManager.AddShot();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            scoreManager.AddHit();
        }
    }
}