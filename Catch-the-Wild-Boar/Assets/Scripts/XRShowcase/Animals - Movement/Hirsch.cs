using UnityEngine;

public class Hirsch : MonoBehaviour
{
    public Transform head;

    public float eatSpeed = 1.4f;
    public float headAngle = 30f;
    public float lookAroundAngle = 15f;

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
        if (head != null)
        {
            float eatMovement = Mathf.Sin(Time.time * eatSpeed) * 8f + headAngle;
            float lookMovement = Mathf.Sin(Time.time * 0.7f) * lookAroundAngle;

            head.localRotation = startHeadRotation * Quaternion.Euler(eatMovement, lookMovement, 0);
        }
    }

    void LateUpdate()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}