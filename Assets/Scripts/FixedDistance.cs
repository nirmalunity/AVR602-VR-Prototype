using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class FixedRayGrabWithDistance
    : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    private bool useRayConstraint = false;
    private Transform interactorTransform;
    private float fixedDistance;
    private Vector3 grabOffset;

    private bool wasRotationTrackEnabled;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactorObject is NearFarInteractor nearFar)
        {
            useRayConstraint = true;
            interactorTransform = (args.interactorObject as IXRInteractor)?.transform;

            fixedDistance = Vector3.Distance(interactorTransform.position, transform.position);

            grabOffset =
                transform.position
                - (interactorTransform.position + interactorTransform.forward * fixedDistance);

            attachTransform = null;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        useRayConstraint = false;
        interactorTransform = null;
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (isSelected && useRayConstraint && interactorTransform != null)
        {
            Vector3 desiredPosition =
                interactorTransform.position
                + interactorTransform.forward * fixedDistance
                + grabOffset;

            transform.position = desiredPosition;

            transform.rotation = interactorTransform.rotation;
        }
    }
}
