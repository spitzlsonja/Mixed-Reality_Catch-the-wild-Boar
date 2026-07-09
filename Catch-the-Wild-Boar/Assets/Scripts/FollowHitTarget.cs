using UnityEngine;

public class FollowHitTarget : MonoBehaviour
{
    private Transform target;
    private Vector3 localPosition;
    private Quaternion localRotation;

    public void Setup(Transform hitTarget)
    {
        target = hitTarget;

        localPosition = target.InverseTransformPoint(transform.position);
        localRotation = Quaternion.Inverse(target.rotation) * transform.rotation;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.TransformPoint(localPosition);
        transform.rotation = target.rotation * localRotation;
    }
}