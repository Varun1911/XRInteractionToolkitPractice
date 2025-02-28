using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] private GameObject leftTeleportationRay;
    [SerializeField] private GameObject rightTeleportationRay;

    [SerializeField] private InputActionProperty leftStick;
    [SerializeField] private InputActionProperty rightStick;
    [SerializeField] private InputActionProperty leftGrab;
    [SerializeField] private InputActionProperty rightGrab;

    [SerializeField] private float activationThreshold = 0.1f;

    public float value = 0;
    private void Update()
    {
        if(leftTeleportationRay)
        {
            leftTeleportationRay.SetActive(leftGrab.action.ReadValue<float>() == 0 &&  leftStick.action.ReadValue<Vector2>().y > activationThreshold);
        }

        if(rightTeleportationRay)
        {
            value = rightStick.action.ReadValue<Vector2>().y;
            rightTeleportationRay.SetActive(rightGrab.action.ReadValue<float>() == 0 && rightStick.action.ReadValue<Vector2>().y > activationThreshold);
        }
    }
}
