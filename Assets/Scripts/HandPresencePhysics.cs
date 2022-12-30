using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider[] handColliders;

    /// <summary>
    /// Set velocity of hand model depending on distance between controller and this model.
    /// Set angular velocity of hand model depending on rotation difference between controller and this model.
    /// </summary>
    private void FixedUpdate()
    {
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        Quaternion rotationDifference = target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);
        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;
        rb.angularVelocity = rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime;
    }

    /// <summary>
    /// Enable all the colliders of the hand.
    /// </summary>
    public void EnableHandCollider()
    {
        foreach (var item in handColliders)
        {
            item.enabled = true;
        }
    }

    /// <summary>
    /// Call the function to enable all colliders of the hand with a delay.
    /// </summary>
    /// <param name="_delay">Time to wait for calling the function.</param>
    public void EnableHandColliderDelay(float _delay)
    {
        Invoke(nameof(EnableHandCollider), _delay);
    }

    /// <summary>
    /// Disable all the colliders of the hand.
    /// </summary>
    public void DisableHandCollider()
    {
        CancelInvoke();

        foreach (var item in handColliders)
        {
            item.enabled = false;
        }
    }
}
