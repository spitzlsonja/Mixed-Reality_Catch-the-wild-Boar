using UnityEngine;

public class Hirsch : MonoBehaviour
{
    public Transform head;

    public float eatSpeed = 1.4f;
    public float headAngle = 30f;
    public float lookAroundAngle = 15f;

    private Quaternion startHeadRotation;
    private float timer;

    void Start()
    {
        if (head != null)
        {
            startHeadRotation = head.localRotation;
        }

        timer = Random.Range(3f, 6f);
    }

    void Update()
    {
        if (head == null) return;

        timer -= Time.deltaTime;

        float eatMovement = Mathf.Sin(Time.time * eatSpeed) * 8f + headAngle;
        float lookMovement = Mathf.Sin(Time.time * 0.7f) * lookAroundAngle;

        head.localRotation = startHeadRotation * Quaternion.Euler(eatMovement, lookMovement, 0);

        if (timer <= 0f)
        {
            timer = Random.Range(3f, 6f);
        }
    }
}