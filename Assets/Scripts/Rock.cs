using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Rock : MonoBehaviour, IInteractable
{
    [SerializeField] private float force = 15f;
    [SerializeField] private XRGrabInteractable xrGrabInteractable;
    [SerializeField] private Rigidbody rb;

    /// <summary>
    /// Start the coroutine to add force to the stone.
    /// </summary>
    public void PrimaryButton()
    {
        StartCoroutine(AddForce());
    }

    /// <summary>
    /// Disable selecting for the controller holding this stone to drop it and add force to the stone after.
    /// In the end re-enable selecting for the controller.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AddForce()
    {
        if (xrGrabInteractable.firstInteractorSelecting.transform.TryGetComponent(out XRBaseControllerInteractor interactor))
        {
            interactor.allowSelect = false;

            rb.AddForce(transform.forward * force);
            rb.AddForce(transform.up * (force / 2));

            yield return new WaitForSeconds(0.1f);

            interactor.allowSelect = true;
        }
    }

    public void SecondaryButton()
    {
    }

    public void TriggerCanceled()
    {
    }

    public void TriggerStarted()
    {
    }
}
