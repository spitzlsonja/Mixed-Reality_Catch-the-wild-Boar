using UnityEngine;

public class Hund : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    public float lookSpeed = 1.2f;
    public float lookAngle = 25f;
    public float tailSpeed = 6f;
    public float tailAngle = 30f;

    private Quaternion startHeadRotation;
    private Quaternion startTailRotation;

    void Start()
    {
        if (head != null)
        {
            startHeadRotation = head.localRotation;
        }

        if (tail != null)
        {
            startTailRotation = tail.localRotation;
        }
    }

    void Update()
    {
        // Kopf schaut links/rechts
        if (head != null)
        {
            float look = Mathf.Sin(Time.time * lookSpeed) * lookAngle;
            head.localRotation = startHeadRotation * Quaternion.Euler(0, look, 0);
        }

        // Schwanz wedelt
        if (tail != null)
        {
            float wag = Mathf.Sin(Time.time * tailSpeed) * tailAngle;
            tail.localRotation = startTailRotation * Quaternion.Euler(0, wag, 0);
        }
    }
}