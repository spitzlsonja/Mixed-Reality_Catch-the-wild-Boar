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

        ContactPoint contact = collision.contacts[0];

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        if (myCollider != null)
        {
            myCollider.enabled = false;
        }

        GameObject arrow = Instantiate(stickingArrow);

        arrow.transform.rotation = transform.rotation;
        arrow.transform.position = contact.point;
        arrow.transform.position -= transform.forward * 0.05f;

        IHittable hittable = collision.collider.GetComponentInParent<IHittable>();

        if (hittable != null)
        {
            // Bei Tieren/Zielen soll der Pfeil mit dem Objekt mitbewegen
            arrow.transform.SetParent(collision.transform, true);

            hittable.GetHit();

            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddHit();
            }
        }
        else
        {
            // Bei Bäumen/Weltobjekten nicht parenten,
            // damit skalierte Bäume den Pfeil nicht verzerren
            arrow.transform.SetParent(null);
            arrow.transform.localScale = Vector3.one;
        }

        Destroy(gameObject);
    }
}