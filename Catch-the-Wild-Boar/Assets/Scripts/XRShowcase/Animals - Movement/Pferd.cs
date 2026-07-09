using UnityEngine;

public class Pferd : MonoBehaviour
{
    public Transform head;

    public float eatSpeed = 1.2f;
    public float headAngle = 25f;
    public float liftHeadEverySeconds = 5f;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private Quaternion startHeadRotation;

    private float timer;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        if (head != null)
        {
            startHeadRotation = head.localRotation;
        }

        timer = Random.Range(2f, liftHeadEverySeconds);
    }

    void Update()
    {
        if (head != null)
        {
            timer -= Time.deltaTime;

            float angle;

            if (timer > 1.5f)
            {
                angle = Mathf.Sin(Time.time * eatSpeed) * 8f + headAngle;
            }
            else
            {
                angle = Mathf.Sin(Time.time * eatSpeed) * 5f - 10f;
            }

            head.localRotation = startHeadRotation * Quaternion.Euler(angle, 0, 0);

            if (timer <= 0f)
            {
                timer = Random.Range(4f, liftHeadEverySeconds);
            }
        }
    }

    void LateUpdate()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}