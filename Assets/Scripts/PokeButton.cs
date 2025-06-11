using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRBaseInteractable))]
public class PokeButton : MonoBehaviour
{
    [SerializeField] private Transform visual;
    [SerializeField] private Vector3 localAxis;
    [SerializeField] private float resetSpeed = 5f;
    [SerializeField] private float followAngleThreshold;

    private XRBaseInteractable interactable;
    private bool isFollowing = false;
    private Vector3 offset;
    private Transform pokeAttachTransform;

    private Vector3 initialPosition;
    private bool isFrozen = false;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
    }

    private void OnEnable()
    {
        interactable.hoverEntered.AddListener(VisualFollow);
        interactable.hoverExited.AddListener(ResetVisual);
        interactable.selectEntered.AddListener(FreezeVisual);
    }

    

    private void OnDisable()
    {
        interactable.hoverEntered.RemoveListener(VisualFollow);
        interactable.hoverExited.RemoveListener(ResetVisual);
        interactable.selectEntered.RemoveListener(FreezeVisual);

    }


    private void Start()
    {
        initialPosition = visual.localPosition;
    }

    private void Update()
    {
        if(isFrozen)
        {
            return;
        }


        if (isFollowing)
        {
            //get position in local space of visual
            Vector3 localTargetPosition = visual.InverseTransformPoint(pokeAttachTransform.position + offset);

            //project the local position in the desired local axis
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition, localAxis);

            //get world position from the constrained local position
            visual.position = visual.TransformPoint(constrainedLocalTargetPosition);
        }

        else
        {
            visual.localPosition =  Vector3.Lerp(visual.localPosition, initialPosition, resetSpeed * Time.deltaTime);
        }
    }

    private void VisualFollow(HoverEnterEventArgs hoverArgs)
    {
        if(hoverArgs.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor) hoverArgs.interactorObject;


            pokeAttachTransform = interactor.attachTransform;
            offset = visual.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, visual.TransformDirection(localAxis));

            if(pokeAngle < followAngleThreshold)
            {
                isFollowing = true;
                isFrozen = false;
            }
        }
    }

    private void FreezeVisual(SelectEnterEventArgs selectArgs)
    {
        if (selectArgs.interactorObject is XRPokeInteractor)
        {
            isFrozen = true;
        }
    }

    private void ResetVisual(HoverExitEventArgs hoverArgs)
    {
        if (hoverArgs.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            isFrozen = false;
        }
    }
}
