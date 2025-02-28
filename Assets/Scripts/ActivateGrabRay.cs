using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ActivateGrabRay : MonoBehaviour
{
    [SerializeField] private GameObject leftGrabRay;
    [SerializeField] private GameObject rightGrabRay;

    [SerializeField] private XRDirectInteractor leftDirectInteractor;
    [SerializeField] private XRDirectInteractor rightDirectInteractor;

    private void Update()
    {
        leftGrabRay.SetActive(leftDirectInteractor.interactablesSelected.Count == 0);
        rightGrabRay.SetActive(rightDirectInteractor.interactablesSelected.Count == 0);
    }
}
