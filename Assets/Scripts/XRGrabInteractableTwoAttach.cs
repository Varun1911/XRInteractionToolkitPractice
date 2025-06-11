using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    [SerializeField] private Transform leftAttachTransform;
    [SerializeField] private Transform rightAttachTransform;

    [SerializeField, Tag] private string rightHandTag;
    [SerializeField, Tag] private string leftHandTag;


    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {

        if (args.interactorObject.transform.CompareTag(rightHandTag) &&  rightAttachTransform != null)
        {
            attachTransform = rightAttachTransform;
        }

        else if (args.interactorObject.transform.CompareTag(leftHandTag) && leftAttachTransform != null)
        {
            attachTransform = leftAttachTransform;
        }

        base.OnSelectEntering(args);
    }

}
