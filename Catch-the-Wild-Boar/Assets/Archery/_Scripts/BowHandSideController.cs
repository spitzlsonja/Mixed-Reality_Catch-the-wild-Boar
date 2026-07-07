using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BowHandSideController : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable bowGrabInteractable;
    [SerializeField] private ArrowController arrowController;

    [Header("Hand-Objekte aus der Scene")]
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

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
        Transform interactor = args.interactorObject.transform;

        Debug.Log("Bogen gegriffen von: " + GetFullPath(interactor));

        bool isLeft =
            interactor == leftHand ||
            interactor.IsChildOf(leftHand) ||
            leftHand.IsChildOf(interactor) ||
            GetFullPath(interactor).ToLower().Contains("left");

        bool isRight =
            interactor == rightHand ||
            interactor.IsChildOf(rightHand) ||
            rightHand.IsChildOf(interactor) ||
            GetFullPath(interactor).ToLower().Contains("right");

        if (isRight)
        {
            arrowController.SetSpawnPoint(spawnPointLeft);
            Debug.Log("SpawnPoint auf LINKS gesetzt");
        }
        else if (isLeft)
        {
            arrowController.SetSpawnPoint(spawnPointRight);
            Debug.Log("SpawnPoint auf RECHTS gesetzt");
        }
        else
        {
            Debug.LogWarning("Hand nicht erkannt. Interactor war: " + GetFullPath(interactor));
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