using UnityEngine;

public class StickingArrowToSurface : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider myCollider;
    [SerializeField] private GameObject stickingArrow;

    [Header("Optional: Pfeilspitze")]
    [SerializeField] private Transform arrowTip;

    [Header("Einstellung")]
    [SerializeField] private float surfaceOffset = 0.05f;

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

        // Rotation vom fliegenden Pfeil übernehmen
        arrow.transform.rotation = transform.rotation;

        // Pfeil platzieren
        if (arrowTip != null)
        {
            Vector3 tipOffset = arrowTip.position - transform.position;
            arrow.transform.position = contact.point - tipOffset;
        }
        else
        {
            arrow.transform.position = contact.point - transform.forward * surfaceOffset;
        }

        // Wichtig: NICHT parenten, damit keine Verzerrung durch skalierte Bäume entsteht
        arrow.transform.SetParent(null);

        // Scale vom Prefab beibehalten
        arrow.transform.localScale = stickingArrow.transform.localScale;

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

            // Pfeil folgt dem getroffenen Körperteil, ohne Child zu werden
            FollowHitTarget follow = arrow.AddComponent<FollowHitTarget>();
            follow.Setup(collision.transform);
        }

        Destroy(gameObject);
    }
}