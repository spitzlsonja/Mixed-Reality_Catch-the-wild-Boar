using UnityEngine;

public class Tiger : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    public float lookSpeed = 0.9f;
    public float lookAngle = 30f;
    public float tailSpeed = 2.5f;
    public float tailAngle = 25f;
    public float bodyTurnAngle = 5f;

    private Quaternion startHeadRotation;
    private Quaternion startTailRotation;
    private Quaternion startBodyRotation;

    void Start()
    {
        startBodyRotation = transform.localRotation;

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
        // Körper dreht sich minimal
        float bodyTurn = Mathf.Sin(Time.time * 0.5f) * bodyTurnAngle;
        transform.localRotation = startBodyRotation * Quaternion.Euler(0, bodyTurn, 0);

        // Kopf schaut herum
        if (head != null)
        {
            float look = Mathf.Sin(Time.time * lookSpeed) * lookAngle;
            head.localRotation = startHeadRotation * Quaternion.Euler(0, look, 0);
        }

        // Schwanz bewegt sich
        if (tail != null)
        {
            float tailMove = Mathf.Sin(Time.time * tailSpeed) * tailAngle;
            tail.localRotation = startTailRotation * Quaternion.Euler(0, tailMove, 0);
        }
    }
}