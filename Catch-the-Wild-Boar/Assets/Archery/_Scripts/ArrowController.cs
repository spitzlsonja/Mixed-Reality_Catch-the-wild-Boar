using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ArrowController : MonoBehaviour
{
    [Header("Arrow Settings")]
    [SerializeField] private GameObject midPointVisual;
    [SerializeField] private GameObject arrowPrefab;

    [Header("Spawn Points")]
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Transform spawnPointLeft;

    [Header("Bow Grab")]
    [SerializeField] private XRGrabInteractable bowGrabInteractable;

    [Header("Power")]
    [SerializeField] private float arrowMaxSpeed = 10;

    [Header("Audio")]
    [SerializeField] private AudioSource bowReleaseAudioSource;

    public void PrepareArrow()
    {
        midPointVisual.SetActive(true);
    }

    public void ReleaseArrow(float strength)
    {
        bowReleaseAudioSource.Play();
        midPointVisual.SetActive(false);

        Transform currentSpawnPoint = GetCorrectSpawnPoint();

        Debug.Log($"Spawne Pfeil bei: {currentSpawnPoint.name} | Weltposition: {currentSpawnPoint.position}");

        GameObject arrow = Instantiate(arrowPrefab);
        arrow.transform.position = currentSpawnPoint.position;
        arrow.transform.rotation = midPointVisual.transform.rotation;

        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(midPointVisual.transform.forward * strength * arrowMaxSpeed, ForceMode.Impulse);

        ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddShot();
        }
    }

    private Transform GetCorrectSpawnPoint()
    {
        if (bowGrabInteractable == null || bowGrabInteractable.firstInteractorSelecting == null)
        {
            return spawnPointRight;
        }

        Transform interactorTransform = bowGrabInteractable.firstInteractorSelecting.transform;

        if (IsRightHand(interactorTransform))
        {
            // Bogen wird rechts gehalten → Pfeil soll links spawnen
            return spawnPointLeft;
        }

        if (IsLeftHand(interactorTransform))
        {
            // Bogen wird links gehalten → Pfeil soll rechts spawnen
            return spawnPointRight;
        }

        return spawnPointRight;
    }

    private bool IsRightHand(Transform t)
    {
        while (t != null)
        {
            string name = t.name.ToLower();

            if (name.Contains("right"))
            {
                return true;
            }

            t = t.parent;
        }

        return false;
    }

    private bool IsLeftHand(Transform t)
    {
        while (t != null)
        {
            string name = t.name.ToLower();

            if (name.Contains("left"))
            {
                return true;
            }

            t = t.parent;
        }

        return false;
    }

    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        // bleibt nur drinnen, falls irgendwo noch ein altes Event darauf zugreift
    }
}