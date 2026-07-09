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

        Debug.Log("BowHandSideController Awake", this);

        Debug.Log("bowGrabInteractable: " + bowGrabInteractable, this);
        Debug.Log("arrowController: " + arrowController, this);
        Debug.Log("leftHand: " + leftHand, this);
        Debug.Log("rightHand: " + rightHand, this);
        Debug.Log("spawnPointRight: " + spawnPointRight, this);
        Debug.Log("spawnPointLeft: " + spawnPointLeft, this);
    }

    private void OnEnable()
    {
        if (bowGrabInteractable != null)
        {
            bowGrabInteractable.selectEntered.AddListener(OnBowGrabbed);
            Debug.Log("Listener wurde registriert", this);
        }
        else
        {
            Debug.LogError("bowGrabInteractable ist NULL!", this);
        }
    }

    private void OnDisable()
    {
        if (bowGrabInteractable != null)
        {
            bowGrabInteractable.selectEntered.RemoveListener(OnBowGrabbed);
        }
    }

    private void OnBowGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("OnBowGrabbed wurde aufgerufen", this);

        if (args.interactorObject == null)
        {
            Debug.LogError("interactorObject ist NULL!", this);
            return;
        }

        if (arrowController == null)
        {
            Debug.LogError("arrowController ist NULL! Bitte im Inspector zuweisen.", this);
            return;
        }

        if (leftHand == null || rightHand == null)
        {
            Debug.LogError("leftHand oder rightHand ist NULL! Bitte im Inspector zuweisen.", this);
            return;
        }

        if (spawnPointLeft == null || spawnPointRight == null)
        {
            Debug.LogError("spawnPointLeft oder spawnPointRight ist NULL! Bitte im Inspector zuweisen.", this);
            return;
        }

        Transform interactor = args.interactorObject.transform;

        string fullPath = GetFullPath(interactor);
        string fullPathLower = fullPath.ToLower();

        Debug.Log("Bogen gegriffen von: " + fullPath, this);
        Debug.Log("Interactor Name: " + interactor.name, this);

        bool isLeft =
            interactor == leftHand ||
            interactor.IsChildOf(leftHand) ||
            leftHand.IsChildOf(interactor) ||
            fullPathLower.Contains("left");

        bool isRight =
            interactor == rightHand ||
            interactor.IsChildOf(rightHand) ||
            rightHand.IsChildOf(interactor) ||
            fullPathLower.Contains("right");

        Debug.Log("isLeft = " + isLeft, this);
        Debug.Log("isRight = " + isRight, this);

        if (isRight)
        {
            arrowController.SetSpawnPoint(spawnPointLeft);
            Debug.Log("Rechte Hand erkannt → SpawnPoint auf LINKS gesetzt: " + spawnPointLeft.name, this);
        }
        else if (isLeft)
        {
            arrowController.SetSpawnPoint(spawnPointRight);
            Debug.Log("Linke Hand erkannt → SpawnPoint auf RECHTS gesetzt: " + spawnPointRight.name, this);
        }
        else
        {
            Debug.LogWarning("Hand nicht erkannt. Interactor war: " + fullPath, this);
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