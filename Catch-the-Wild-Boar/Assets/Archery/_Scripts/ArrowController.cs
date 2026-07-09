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

    public Transform VisualArrow;

    private Transform currentSpawnPoint;

    private void Awake()
    {
        currentSpawnPoint = spawnPointRight;

        Debug.Log("ArrowController Awake", this);
        Debug.Log("spawnPointRight: " + spawnPointRight, this);
        Debug.Log("spawnPointLeft: " + spawnPointLeft, this);
        Debug.Log("currentSpawnPoint Startwert: " + currentSpawnPoint, this);
    }

    public void PrepareArrow()
    {
        if (midPointVisual != null)
        {
            midPointVisual.SetActive(true);
        }
    }

    public void ReleaseArrow(float strength)
    {
        if (bowReleaseAudioSource != null)
        {
            bowReleaseAudioSource.Play();
        }

        if (midPointVisual != null)
        {
            midPointVisual.SetActive(false);
        }

        if (arrowPrefab == null)
        {
            Debug.LogError("arrowPrefab ist NULL! Bitte im Inspector zuweisen.", this);
            return;
        }

        if (currentSpawnPoint == null)
        {
            Debug.LogWarning("currentSpawnPoint war NULL. Verwende spawnPointRight als Fallback.", this);
            currentSpawnPoint = spawnPointRight;
        }

        if (currentSpawnPoint == null)
        {
            Debug.LogError("Kein SpawnPoint vorhanden!", this);
            return;
        }

        Debug.Log("Spawne Pfeil bei: " + currentSpawnPoint.name + 
                  " | Weltposition: " + currentSpawnPoint.position, this);

        GameObject arrow = Instantiate(
            arrowPrefab,
            currentSpawnPoint.position,
            midPointVisual.transform.rotation
        );

        Transform arrowTip = FindChildByName(arrow.transform, "ArrowTip");

        if (arrowTip != null)
        {
            Vector3 offset = currentSpawnPoint.position - arrowTip.position;
            arrow.transform.position += offset;

            Debug.Log("ArrowTip wurde exakt auf SpawnPoint gesetzt: " + currentSpawnPoint.name, this);
        }
        else
        {
            Debug.LogWarning("ArrowTip wurde im Pfeil-Prefab nicht gefunden!", arrow);
        }

        Rigidbody rb = arrow.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(midPointVisual.transform.forward * strength * arrowMaxSpeed, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Der Pfeil hat keinen Rigidbody!", arrow);
        }

        GameStartManager gameStartManager = FindFirstObjectByType<GameStartManager>();

        if (gameStartManager != null && gameStartManager.IsGameRunning())
        {
            ScoreManager scoreManager = FindFirstObjectByType<ScoreManager>();

            if (scoreManager != null)
            {
                scoreManager.AddShot();
            }
        }
    }

    public void SetSpawnPoint(Transform newSpawnPoint)
    {
        if (newSpawnPoint == null)
        {
            Debug.LogError("SetSpawnPoint wurde mit NULL aufgerufen!", this);
            return;
        }

        currentSpawnPoint = newSpawnPoint;
        VisualArrow.position = currentSpawnPoint.position;

        Debug.Log("ArrowController SpawnPoint geändert auf: " + currentSpawnPoint.name, this);
    }
    
    private Transform FindChildByName(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
            {
                return child;
            }

            Transform found = FindChildByName(child, childName);

            if (found != null)
            {
                return found;
            }
        }

        return null;
    }
}