using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabHandPose : MonoBehaviour
{
    [SerializeField] private HandData rightHandPose;

    XRGrabInteractable xrGrabInteractable;

    private Vector3 startingHandPosition;
    private Quaternion startingHandRotation;
    private Vector3 finalHandPosition;
    private Quaternion finalHandRotation;
    private Quaternion[] startingFingerRotations;
    private Quaternion[] finalFingerRotations;

    private void Awake()
    {
        xrGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void Start()
    {
        rightHandPose.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        xrGrabInteractable.selectEntered.AddListener(SetupPose);
        xrGrabInteractable.selectExited.AddListener(ResetPose);
    }

    private void OnDisable()
    {
        xrGrabInteractable.selectEntered.RemoveListener(SetupPose);
        xrGrabInteractable.selectExited.AddListener(ResetPose);
    }


    public void SetupPose(BaseInteractionEventArgs args)
    {
        if(args.interactorObject is XRDirectInteractor)
        {
            HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.Animator.enabled = false;

            SetHandDataValues(handData, rightHandPose);
            SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);
        }
    }

    private void SetHandDataValues(HandData toHand ,HandData fromHand)
    {
        Vector3 scaleToHand = toHand.Root.localScale;
        Vector3 scaleFromHand = fromHand.Root.localScale;
        //scaleFromHand = Vector3.one * 0.25f;


        //startingHandPosition = new Vector3(toHand.Root.localPosition.x / scaleToHand.x, toHand.Root.localPosition.y / scaleToHand.y, toHand.Root.localPosition.z / scaleToHand.z);
        //finalHandPosition = new Vector3(fromHand.Root.localPosition.x / scaleFromHand.x, fromHand.Root.localPosition.y / scaleFromHand.y, fromHand.Root.localPosition.z / scaleFromHand.z);
        startingHandPosition = toHand.Root.localPosition;
        finalHandPosition = fromHand.Root.localPosition;

        startingHandRotation = toHand.Root.localRotation;
        finalHandRotation = fromHand.Root.localRotation;

        int length = toHand.FingerTransforms.Length;
        startingFingerRotations = new Quaternion[length];
        finalFingerRotations = new Quaternion[length];

        for(int i = 0; i < length; i++)
        {
            startingFingerRotations[i] = fromHand.FingerTransforms[i].localRotation;   
            finalFingerRotations[i] = toHand.FingerTransforms[i].localRotation;   
        }
    }


    private void SetHandData(HandData handData, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBoneRotations)
    {
        handData.Root.localPosition = newPosition;
        handData.Root.localRotation = newRotation;

        for(int i = 0; i < newBoneRotations.Length; i++)
        {
            handData.FingerTransforms[i].localRotation = newBoneRotations[i];
        }
    }

    private void ResetPose(SelectExitEventArgs args)
    {
        if (args.interactorObject is XRDirectInteractor)
        {
            HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.Animator.enabled = true;

            SetHandData(handData, startingHandPosition, startingHandRotation, startingFingerRotations);
        }
    }
}
