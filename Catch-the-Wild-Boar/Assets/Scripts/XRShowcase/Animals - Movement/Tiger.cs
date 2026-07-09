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

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Quaternion startHeadRotation;
    private Quaternion startTailRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

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
        // Körper dreht sich minimal, bleibt aber am Platz
        float bodyTurn = Mathf.Sin(Time.time * 0.5f) * bodyTurnAngle;
        transform.rotation = startRotation * Quaternion.Euler(0, bodyTurn, 0);

        if (head != null)
        {
            float look = Mathf.Sin(Time.time * lookSpeed) * lookAngle;
            head.localRotation = startHeadRotation * Quaternion.Euler(0, look, 0);
        }

        if (tail != null)
        {
            float tailMove = Mathf.Sin(Time.time * tailSpeed) * tailAngle;
            tail.localRotation = startTailRotation * Quaternion.Euler(0, tailMove, 0);
        }
    }

    void LateUpdate()
    {
        transform.position = startPosition;
    }
}