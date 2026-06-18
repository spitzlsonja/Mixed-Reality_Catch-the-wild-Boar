using UnityEngine;

public class Pferd : MonoBehaviour
{
    public Transform head;

    public float eatSpeed = 1.2f;
    public float headAngle = 25f;
    public float liftHeadEverySeconds = 5f;

    private Quaternion startHeadRotation;
    private float timer;

    void Start()
    {
        if (head != null)
        {
            startHeadRotation = head.localRotation;
        }

        timer = Random.Range(2f, liftHeadEverySeconds);
    }

    void Update()
    {
        if (head == null) return;

        timer -= Time.deltaTime;

        float angle;

        if (timer > 1.5f)
        {
            // Kopf unten, frisst Gras
            angle = Mathf.Sin(Time.time * eatSpeed) * 8f + headAngle;
        }
        else
        {
            // Kopf kurz heben
            angle = Mathf.Sin(Time.time * eatSpeed) * 5f - 10f;
        }

        head.localRotation = startHeadRotation * Quaternion.Euler(angle, 0, 0);

        if (timer <= 0f)
        {
            timer = Random.Range(4f, liftHeadEverySeconds);
        }
    }
}