using UnityEngine;

public class Pferd : MonoBehaviour
{
    public Transform head; // Kopf oder Hals vom Pferd hier reinziehen

    public float eatSpeed = 1.2f;
    public float headDownAngle = 30f;
    public float bodyMoveAmount = 0.02f;

    public float walkSpeed = 0.4f;
    public float maxWalkDistance = 2f;
    public float walkChance = 0.3f;

    private Vector3 startPosition;
    private Quaternion startHeadRotation;

    private bool isWalking = false;
    private float walkTimer = 0f;
    private float nextActionTimer = 0f;

    void Start()
    {
        startPosition = transform.position;

        if (head != null)
        {
            startHeadRotation = head.localRotation;
        }

        nextActionTimer = Random.Range(2f, 5f);
    }

    void Update()
    {
        EatMovement();
        SmallBodyMovement();
        RandomWalking();
    }

    void EatMovement()
    {
        if (head != null)
        {
            // Kopf langsam hoch und runter bewegen
            float angle = Mathf.Sin(Time.time * eatSpeed) * headDownAngle;

            // meistens Kopf eher unten halten
            head.localRotation = startHeadRotation * Quaternion.Euler(angle + 20f, 0, 0);
        }
    }

    void SmallBodyMovement()
    {
        // ganz leichte Bewegung, damit es nicht statisch wirkt
        float bodyY = Mathf.Sin(Time.time * eatSpeed) * bodyMoveAmount;
        transform.position = new Vector3(transform.position.x, startPosition.y + bodyY, transform.position.z);
    }

    void RandomWalking()
    {
        nextActionTimer -= Time.deltaTime;

        if (!isWalking && nextActionTimer <= 0f)
        {
            if (Random.value < walkChance)
            {
                isWalking = true;
                walkTimer = Random.Range(1f, 3f);
            }

            nextActionTimer = Random.Range(4f, 8f);
        }

        if (isWalking)
        {
            walkTimer -= Time.deltaTime;

            // nur nach vorne gehen, aber nicht zu weit
            float distanceFromStart = Vector3.Distance(
                new Vector3(transform.position.x, 0, transform.position.z),
                new Vector3(startPosition.x, 0, startPosition.z)
            );

            if (distanceFromStart < maxWalkDistance)
            {
                transform.position += transform.forward * walkSpeed * Time.deltaTime;
            }

            if (walkTimer <= 0f || distanceFromStart >= maxWalkDistance)
            {
                isWalking = false;
            }
        }
    }
}