using UnityEngine;

public class Katze : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    public float lookSpeed = 1f;
    public float lookAngle = 25f;
    public float tailSpeed = 2f;
    public float tailAngle = 20f;

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
        transform.rotation = startRotation;
    }
}