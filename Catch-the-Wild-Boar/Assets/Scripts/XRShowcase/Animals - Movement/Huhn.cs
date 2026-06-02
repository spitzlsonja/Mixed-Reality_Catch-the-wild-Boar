using UnityEngine;

public class Huhn : MonoBehaviour
{
    public Transform head;

    public float peckSpeed = 4f;
    public float peckAngle = 25f;
    public float walkSpeed = 0.5f;
    public float maxWalkDistance = 2f;

    private Vector3 startPosition;
    private Quaternion startHeadRotation;
    private float actionTimer;

    void Start()
    {
        startPosition = transform.position;

        if (head != null)
            startHeadRotation = head.localRotation;

        actionTimer = Random.Range(2f, 5f);
    }

    void Update()
    {
        Peck();
        SmallWalk();
    }

    void Peck()
    {
        if (head != null)
        {
            float angle = Mathf.Abs(Mathf.Sin(Time.time * peckSpeed)) * peckAngle;
            head.localRotation = startHeadRotation * Quaternion.Euler(angle, 0, 0);
        }
    }

    void SmallWalk()
    {
        actionTimer -= Time.deltaTime;

        if (actionTimer <= 0f)
        {
            transform.Rotate(0, Random.Range(-90f, 90f), 0);
            actionTimer = Random.Range(2f, 5f);
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