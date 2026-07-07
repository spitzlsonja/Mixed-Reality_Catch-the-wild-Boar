using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickingArrowToSurface : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private SphereCollider myCollider;

    [SerializeField]
    private GameObject stickingArrow;

    private void OnCollisionEnter(Collision collision)
    {
        rb.isKinematic = true;
        myCollider.isTrigger = true;

        GameObject arrow = Instantiate(stickingArrow);
        arrow.transform.position = transform.position;
        arrow.transform.forward = transform.forward;

        if (collision.collider.attachedRigidbody != null)
        {
            arrow.transform.parent = collision.collider.attachedRigidbody.transform;
        }

        IHittable hittable = collision.collider.GetComponent<IHittable>();

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