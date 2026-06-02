using UnityEngine;

public class Schmetterling : MonoBehaviour
{
    public float speed = 1.5f;
    public float flyHeight = 0.3f;
    public float flySpeed = 2f;
    public float turnSpeed = 30f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Schmetterling fliegt nach vorne
        transform.position += transform.forward * speed * Time.deltaTime;

        // Schmetterling bewegt sich leicht auf und ab
        float y = startPosition.y + Mathf.Sin(Time.time * flySpeed) * flyHeight;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);

        // Schmetterling dreht sich langsam
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}