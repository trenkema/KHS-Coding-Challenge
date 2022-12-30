using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private HingeJoint hinge;
    [SerializeField] private UnityEvent onMinLimit;
    [SerializeField] private UnityEvent onMaxLimit;

    private bool isSelected = false;
    private bool hasReachedLimit = false;

    /// <summary>
    /// Check for the angle of hinge when this lever is selected.
    /// </summary>
    private void Update()
    {
        if (isSelected)
        {
            CheckAngle();
        }
    }

    /// <summary>
    /// Checks if the angle of the hinge is equal or over the limit.
    /// If so invoke an event.
    /// </summary>
    private void CheckAngle()
    {
        if (!hasReachedLimit)
        {
            if (hinge.angle <= hinge.limits.min)
            {
                hasReachedLimit = true;
                onMinLimit?.Invoke();
            }
            else if (hinge.angle >= hinge.limits.max)
            {
                hasReachedLimit = true;
                onMaxLimit?.Invoke();
            }
        }
        else
        {
            if (hinge.angle != hinge.limits.min || hinge.angle != hinge.limits.max)
            {
                hasReachedLimit = false;
            }
        }
    }

    /// <summary>
    /// Only freeze the X and Y rotation when selecting the lever.
    /// </summary>
    public void SelectLever()
    {
        isSelected = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }

    /// <summary>
    /// Freeze all the constraints of the rigidbody when deselecting the lever.
    /// </summary>
    public void DeSelectLever()
    {
        isSelected = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
