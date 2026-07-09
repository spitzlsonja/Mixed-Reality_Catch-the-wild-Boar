using UnityEngine;

public class TierPositionFix : MonoBehaviour
{
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void LateUpdate()
    {
        // Tier bleibt immer an der Startposition
        transform.position = startPosition;
    }
}