using UnityEngine;

public class Hund : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    public float lookSpeed = 1.5f;
    public float tailSpeed = 6f;
    public float walkSpeed = 0.4f;
    public float maxWalkDistance = 2.5f;

    private Vector3 startPosition;
    private Quaternion startHeadRotation;
    private Quaternion startTailRotation;
    private float walkTimer;

    void Start()
    {
        startPosition = transform.position;

        if (head != null)
            startHeadRotation = head.localRotation;

        if (tail != null)
            startTailRotation = tail.localRotation;

        walkTimer = Random.Range(3f, 6f);
    }

    void Update()
    {
        LookAround();
        WagTail();
        SmallWalk();
    }

    void LookAround()
    {
        if (head != null)
        {
            float angle = Mathf.Sin(Time.time * lookSpeed) * 25f;
            head.localRotation = startHeadRotation * Quaternion.Euler(0, angle, 0);
        }
    }

    void WagTail()
    {
        if (tail != null)
        {
            float angle = Mathf.Sin(Time.time * tailSpeed) * 35f;
            tail.localRotation = startTailRotation * Quaternion.Euler(0, angle, 0);
        }
    }

    void SmallWalk()
    {
        walkTimer -= Time.deltaTime;

        if (walkTimer <= 0f)
        {
            transform.Rotate(0, Random.Range(-60f, 60f), 0);
            walkTimer = Random.Range(3f, 7f);
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