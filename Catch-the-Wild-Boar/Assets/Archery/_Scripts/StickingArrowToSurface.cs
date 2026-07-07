using UnityEngine;

public class StickingArrowToSurface : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider myCollider;
    [SerializeField] private GameObject stickingArrow;

    private bool hasHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        // Ersten echten Trefferpunkt holen
        ContactPoint contact = collision.contacts[0];

        // Bewegung stoppen
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (myCollider != null)
        {
            myCollider.isTrigger = true;
        }

        // Steckenden Pfeil erstellen
        GameObject arrow = Instantiate(stickingArrow);

        // Pfeil genau am Trefferpunkt platzieren
        arrow.transform.position = contact.point;

        // Pfeilrichtung übernehmen
        arrow.transform.rotation = transform.rotation;

        // Ganz leicht aus der Oberfläche heraussetzen,
        // damit er nicht im Collider flackert
        arrow.transform.position -= transform.forward * 0.05f;

        // An getroffenes Objekt hängen
        arrow.transform.SetParent(collision.transform, true);

        // Trefferlogik
        IHittable hittable = collision.collider.GetComponentInParent<IHittable>();

        if (hittable != null)
        {
            hittable.GetHit();

            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddHit();
            }
        }

        Destroy(gameObject);
    }
}