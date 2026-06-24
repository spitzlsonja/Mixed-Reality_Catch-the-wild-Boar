using UnityEngine;

public class Huhn : MonoBehaviour
{
    public Transform head;

    public float peckSpeed = 5f;
    public float peckAngle = 35f;
    public float bodyTurnAngle = 8f;

    private Quaternion startHeadRotation;
    private Quaternion startBodyRotation;

    void Start()
    {
        startBodyRotation = transform.localRotation;

        if (head != null)
        {
            startHeadRotation = head.localRotation;
        }
    }

    void Update()
    {
        // Körper dreht sich minimal links/rechts
        float bodyY = Mathf.Sin(Time.time * 0.8f) * bodyTurnAngle;
        transform.localRotation = startBodyRotation * Quaternion.Euler(0, bodyY, 0);

        // Kopf pickt nach unten
        if (head != null)
        {
            float angle = Mathf.Abs(Mathf.Sin(Time.time * peckSpeed)) * peckAngle;
            head.localRotation = startHeadRotation * Quaternion.Euler(angle, 0, 0);
        }
    }
}