using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BowHandSideController : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable bowGrabInteractable;
    [SerializeField] private ArrowController arrowController;

    [Header("Direkte Interactor-Objekte aus dem XR Rig")]
    [SerializeField] private Transform leftHandInteractor;
    [SerializeField] private Transform rightHandInteractor;

    [Header("Spawn Points am Bogen")]
    [SerializeField] private Transform spawnPointRight;
    [SerializeField] private Transform spawnPointLeft;

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

        Debug.Log("Bogen gegriffen von: " + interactorTransform.name);

        bool grabbedWithLeftHand =
            interactorTransform == leftHandInteractor ||
            interactorTransform.IsChildOf(leftHandInteractor) ||
            interactorTransform.name.ToLower().Contains("left");

        bool grabbedWithRightHand =
            interactorTransform == rightHandInteractor ||
            interactorTransform.IsChildOf(rightHandInteractor) ||
            interactorTransform.name.ToLower().Contains("right");

        if (grabbedWithRightHand)
        {
            arrowController.SetSpawnPoint(spawnPointLeft);
            Debug.Log("Spawn Point auf LINKS gesetzt");
        }
        else if (grabbedWithLeftHand)
        {
            arrowController.SetSpawnPoint(spawnPointRight);
            Debug.Log("Spawn Point auf RECHTS gesetzt");
        }
        else
        {
            Debug.LogWarning("Hand konnte nicht erkannt werden. Aktueller Interactor: " + interactorTransform.name);
        }
    }
}