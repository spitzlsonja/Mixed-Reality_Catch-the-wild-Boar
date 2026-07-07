using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BowHandSideController : MonoBehaviour
{
    [SerializeField]
    private XRGrabInteractable bowGrabInteractable;

    [SerializeField]
    private ArrowController arrowController;

    [Header("Hand-Referenzen aus dem XR Rig")]
    [SerializeField]
    private Transform leftHandController;
    [SerializeField]
    private Transform rightHandController;

    [Header("Spawn Points")]
    [SerializeField]
    private Transform spawnPointRight; // Standard: Bogen mit Linker Hand gegriffen
    [SerializeField]
    private Transform spawnPointLeft;  // Bogen mit Rechter Hand gegriffen

    private void Awake()
    {
        if (bowGrabInteractable == null)
            bowGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        bowGrabInteractable.selectEntered.AddListener(OnBowGrabbed);
    }

    private void OnDisable()
    {
        bowGrabInteractable.selectEntered.RemoveListener(OnBowGrabbed);
    }

    private void OnBowGrabbed(SelectEnterEventArgs args)
    {
        Transform interactorTransform = args.interactorObject.transform;

        Debug.Log($"Bogen gegriffen von: {interactorTransform.name} (Pfad: {GetFullPath(interactorTransform)})");

        bool grabbedWithLeftHand = leftHandController != null &&
                                   interactorTransform.IsChildOf(leftHandController);

        bool grabbedWithRightHand = rightHandController != null &&
                                    interactorTransform.IsChildOf(rightHandController);

        Debug.Log($"Links erkannt: {grabbedWithLeftHand} | Rechts erkannt: {grabbedWithRightHand}");

        if (grabbedWithRightHand)
        {
            arrowController.SetSpawnPoint(spawnPointLeft);
            Debug.Log("→ Spawn Point auf LINKS gesetzt");
        }
        else if (grabbedWithLeftHand)
        {
            arrowController.SetSpawnPoint(spawnPointRight);
            Debug.Log("→ Spawn Point auf RECHTS gesetzt");
        }
        else
        {
            Debug.LogWarning("→ Weder linke noch rechte Hand erkannt!");
        }
    }

    private string GetFullPath(Transform t)
    {
        string path = t.name;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }
}