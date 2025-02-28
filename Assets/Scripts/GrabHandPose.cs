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
    }

    private void OnDisable()
    {
        xrGrabInteractable.selectEntered.RemoveListener(SetupPose); 
    }

    public void SetupPose(BaseInteractionEventArgs args)
    {
        if(args.interactorObject is XRDirectInteractor)
        {
            HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
            handData.GetAnimator().enabled = false;

            SetHandDataValues(handData, rightHandPose);
            SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotations);
        }
    }

    private void SetHandDataValues(HandData toHand ,HandData fromHand)
    {
        Vector3 scaleToHand = toHand.GetRoot().localScale;
        Vector3 scaleFromHand = fromHand.GetRoot().localScale;
        //scaleFromHand = Vector3.one * 0.25f;

        //Debug.Log(scaleToHand);
        //Debug.Log(scaleFromHand);
        startingHandPosition = new Vector3(toHand.GetRoot().localPosition.x / scaleToHand.x, toHand.GetRoot().localPosition.y / scaleToHand.y, toHand.GetRoot().localPosition.z / scaleToHand.z);
        finalHandPosition = new Vector3(fromHand.GetRoot().localPosition.x / scaleFromHand.x, fromHand.GetRoot().localPosition.y / scaleFromHand.y, fromHand.GetRoot().localPosition.z / scaleFromHand.z);

        startingHandRotation = toHand.GetRoot().localRotation;
        finalHandRotation = fromHand.GetRoot().localRotation;

        int length = toHand.GetFingerTransforms().Length;
        startingFingerRotations = new Quaternion[length];
        finalFingerRotations = new Quaternion[length];

        for(int i = 0; i < length; i++)
        {
            startingFingerRotations[i] = fromHand.GetFingerTransforms()[i].localRotation;   
            finalFingerRotations[i] = toHand.GetFingerTransforms()[i].localRotation;   
        }
    }


    private void SetHandData(HandData handData, Vector3 newPosition, Quaternion newRotation, Quaternion[] newBoneRotations)
    {
        handData.GetRoot().localPosition = newPosition;
        handData.GetRoot().localRotation = newRotation;

        for(int i = 0; i < newBoneRotations.Length; i++)
        {
            handData.GetFingerTransforms()[i].localRotation = newBoneRotations[i];
        }
    }
}
