using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XROffsetGrabInteractable : XRGrabInteractable
{
    /// <summary>
    /// Create an attachTransform if not existing yet.
    /// Make it a parent of this object and set the transform to it.
    /// </summary>
    private void Start()
    {
        if (!attachTransform)
        {
            GameObject attachPoint = new GameObject("Offset Grab Pivot");
            attachPoint.transform.SetParent(transform, false);
            attachTransform = attachPoint.transform;
        }
    }

    /// <summary>
    /// Set the attachTransform position and rotation to the interacted object.
    /// </summary>
    /// <param name="args">Information about the object picked up</param>
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        attachTransform.position = args.interactorObject.transform.position;
        attachTransform.rotation = args.interactorObject.transform.rotation;

        base.OnSelectEntered(args);
    }
}
