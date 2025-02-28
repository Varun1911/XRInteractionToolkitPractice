using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerHaptics : MonoBehaviour
{
    //We can use the simple haptic feedback script to add haptics on certain events triggered by an interactor but if we want haptics otherwise?
    [SerializeField, Range(0,1)] private float intensity;
    [SerializeField] private float duration;


    public void TriggerHaptics()
    {
        if(intensity > 0)
        {
                
        }
    }

}
