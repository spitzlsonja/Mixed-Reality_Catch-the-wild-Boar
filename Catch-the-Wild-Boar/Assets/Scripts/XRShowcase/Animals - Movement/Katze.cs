using UnityEngine;

public class Katze : MonoBehaviour
{
    public Transform head;
    public Transform tail;

    public float lookSpeed = 1.2f;
    public float tailMoveSpeed = 2f;
    public float walkSpeed = 0.35f;
    public float maxWalkDistance = 2f;

    private Vector3 startPosition;
    private Quaternion startHeadRotation;
    private Quaternion startTailRotation;
    private float pauseTimer;

    void Start()
    {
        startPosition = transform.position;

        if (head != null)
            startHeadRotation = head.localRotation;

        if (tail != null)
            startTailRotation = tail.localRotation;

        pauseTimer = Random.Range(2f, 5f);
    }

    void Update()
    {
        LookAround();
        MoveTail();
        SlowWalk();
    }

    void LookAround()
    {
        if (head != null)
        {
            float angle = Mathf.Sin(Time.time * lookSpeed) * 30f;
            head.localRotation = startHeadRotation * Quaternion.Euler(0, angle, 0);
        }
    }

    void MoveTail()
    {
        if (tail != null)
        {
            float angle = Mathf.Sin(Time.time * tailMoveSpeed) * 20f;
            tail.localRotation = startTailRotation * Quaternion.Euler(0, angle, 0);
        }
    }

    void SlowWalk()
    {
        pauseTimer -= Time.deltaTime;

        if (pauseTimer <= 0f)
        {
            transform.Rotate(0, Random.Range(-80f, 80f), 0);
            pauseTimer = Random.Range(3f, 6f);
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