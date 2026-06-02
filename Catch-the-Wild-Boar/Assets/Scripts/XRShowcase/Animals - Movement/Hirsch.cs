using UnityEngine;

public class Hirsch : MonoBehaviour
{
    public Transform head; // Kopf oder Hals vom Hirsch hier reinziehen

    public float headMoveSpeed = 1.5f;
    public float headDownAngle = 35f;
    public float bodyMoveAmount = 0.03f;

    private Quaternion startHeadRotation;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;

        if (head != null)
        {
            startHeadRotation = head.localRotation;
        }
    }

    void Update()
    {
        // Körper bewegt sich ganz leicht auf und ab
        float bodyY = Mathf.Sin(Time.time * headMoveSpeed) * bodyMoveAmount;
        transform.position = startPosition + new Vector3(0, bodyY, 0);

        // Kopf bewegt sich nach unten und oben, als würde er Gras fressen
        if (head != null)
        {
            float angle = Mathf.Sin(Time.time * headMoveSpeed) * headDownAngle;
            head.localRotation = startHeadRotation * Quaternion.Euler(angle, 0, 0);
        }
    }
}