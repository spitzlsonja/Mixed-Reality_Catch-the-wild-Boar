using UnityEngine;

public class Tiger : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    public float walkSpeed = 0.45f;
    public float maxWalkDistance = 3f;
    public float headMoveSpeed = 1.2f;
    public float tailMoveSpeed = 2.5f;

    private Vector3 startPosition;
    private Quaternion startHeadRotation;
    private Quaternion startTailRotation;
    private float actionTimer;

    void Start()
    {
        startPosition = transform.position;

        if (head != null)
            startHeadRotation = head.localRotation;

        if (tail != null)
            startTailRotation = tail.localRotation;

        actionTimer = Random.Range(3f, 6f);
    }

    void Update()
    {
        LookAround();
        MoveTail();
        WalkSmallArea();
    }

    void LookAround()
    {
        if (head != null)
        {
            float angle = Mathf.Sin(Time.time * headMoveSpeed) * 25f;
            head.localRotation = startHeadRotation * Quaternion.Euler(0, angle, 0);
        }
    }

    void MoveTail()
    {
        if (tail != null)
        {
            float angle = Mathf.Sin(Time.time * tailMoveSpeed) * 25f;
            tail.localRotation = startTailRotation * Quaternion.Euler(0, angle, 0);
        }
    }

    void WalkSmallArea()
    {
        actionTimer -= Time.deltaTime;

        if (actionTimer <= 0f)
        {
            transform.Rotate(0, Random.Range(-70f, 70f), 0);
            actionTimer = Random.Range(4f, 8f);
        }

        float distance = Vector3.Distance(startPosition, transform.position);

        if (distance < maxWalkDistance)
        {
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }
        else
        {
            transform.LookAt(startPosition);
        }
    }
}