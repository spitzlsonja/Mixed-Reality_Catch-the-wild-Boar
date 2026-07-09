using UnityEngine;

public class Huhn : MonoBehaviour
{
    public Transform head;

    public float peckSpeed = 5f;
    public float peckAngle = 35f;
    public float bodyTurnAngle = 8f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Quaternion startHeadRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        if (head != null)
        {
            startHeadRotation = head.localRotation;
        }
    }

    void Update()
    {
        // Körper dreht sich minimal links/rechts, bleibt aber am Platz
        float bodyY = Mathf.Sin(Time.time * 0.8f) * bodyTurnAngle;
        transform.rotation = startRotation * Quaternion.Euler(0, bodyY, 0);

        // Kopf pickt nach unten
        if (head != null)
        {
            float angle = Mathf.Abs(Mathf.Sin(Time.time * peckSpeed)) * peckAngle;
            head.localRotation = startHeadRotation * Quaternion.Euler(angle, 0, 0);
        }
    }

    void LateUpdate()
    {
        transform.position = startPosition;
    }
}