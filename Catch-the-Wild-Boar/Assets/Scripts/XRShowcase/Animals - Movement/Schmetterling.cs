using UnityEngine;

public class Schmetterling : MonoBehaviour
{
    public Transform leftWing;
    public Transform rightWing;

    public float hoverSpeed = 2f;
    public float hoverHeight = 0.2f;
    public float wingSpeed = 12f;
    public float wingAngle = 35f;
    public float rotateSpeed = 20f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Quaternion startLeftWingRotation;
    private Quaternion startRightWingRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        if (leftWing != null)
        {
            startLeftWingRotation = leftWing.localRotation;
        }

        if (rightWing != null)
        {
            startRightWingRotation = rightWing.localRotation;
        }
    }

    void Update()
    {
        // schwebt nur auf der Stelle
        float y = Mathf.Sin(Time.time * hoverSpeed) * hoverHeight;
        transform.position = startPosition + new Vector3(0, y, 0);

        // dreht sich leicht
        transform.rotation = startRotation * Quaternion.Euler(0, Mathf.Sin(Time.time * 0.6f) * rotateSpeed, 0);

        // Flügel schlagen
        if (leftWing != null && rightWing != null)
        {
            float angle = Mathf.Sin(Time.time * wingSpeed) * wingAngle;

            leftWing.localRotation = startLeftWingRotation * Quaternion.Euler(0, angle, 0);
            rightWing.localRotation = startRightWingRotation * Quaternion.Euler(0, -angle, 0);
        }
    }
}