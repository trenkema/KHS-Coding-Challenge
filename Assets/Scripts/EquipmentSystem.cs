using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] private InputActionReference leftPrimaryActionReference;
    [SerializeField] private InputActionReference rightPrimaryActionReference;
    [SerializeField] private InputActionReference leftSecondaryActionReference;
    [SerializeField] private InputActionReference rightSecondaryActionReference;
    [SerializeField] private InputActionReference leftTriggerActionReference;
    [SerializeField] private InputActionReference rightTriggerActionReference;

    private IInteractable leftEquipedInteractable;
    private IInteractable rightEquipedInteractable;

    private void OnEnable()
    {
        leftPrimaryActionReference.action.started += LeftPrimary;
        rightPrimaryActionReference.action.started += RightPrimary;
        leftSecondaryActionReference.action.started += LeftSecondary;
        rightSecondaryActionReference.action.started += RightSecondary;
        leftTriggerActionReference.action.started += LeftTriggerStarted;
        rightTriggerActionReference.action.started += RightTriggerStarted;
        leftTriggerActionReference.action.canceled += LeftTriggerCanceled;
        rightTriggerActionReference.action.canceled += RightTriggerCanceled;
    }

    private void LeftPrimary(InputAction.CallbackContext _context)
    {
        if (leftEquipedInteractable != null)
        {
            leftEquipedInteractable.PrimaryButton();
        }
    }

    private void RightPrimary(InputAction.CallbackContext _context)
    {
        if (rightEquipedInteractable != null)
        {
            rightEquipedInteractable.PrimaryButton();
        }
    }

    private void LeftSecondary(InputAction.CallbackContext _context)
    {
        if (leftEquipedInteractable != null)
        {
            leftEquipedInteractable.SecondaryButton();
        }
    }

    private void RightSecondary(InputAction.CallbackContext _context)
    {
        if (rightEquipedInteractable != null)
        {
            rightEquipedInteractable.SecondaryButton();
        }
    }

    private void LeftTriggerStarted(InputAction.CallbackContext _context)
    {
        if (leftEquipedInteractable != null)
        {
            leftEquipedInteractable.TriggerStarted();
        }
    }

    private void RightTriggerStarted(InputAction.CallbackContext _context)
    {
        if (rightEquipedInteractable != null)
        {
            rightEquipedInteractable.TriggerStarted();
        }
    }

    private void LeftTriggerCanceled(InputAction.CallbackContext _context)
    {
        if (leftEquipedInteractable != null)
        {
            leftEquipedInteractable.TriggerCanceled();
        }
    }

    private void RightTriggerCanceled(InputAction.CallbackContext _context)
    {
        if (rightEquipedInteractable != null)
        {
            rightEquipedInteractable.TriggerCanceled();
        }
    }

    public void AddLeftInteractable(SelectEnterEventArgs _args)
    {
        if (_args.interactableObject.transform.TryGetComponent(out IInteractable useable))
        {
            leftEquipedInteractable = useable;
        }
    }

    public void RemoveLeftInteractable(SelectExitEventArgs _args)
    {
        if (_args.interactableObject.transform.TryGetComponent(out IInteractable useable))
        {
            leftEquipedInteractable = null;
        }
    }

    public void AddRightInteractable(SelectEnterEventArgs _args)
    {
        if (_args.interactableObject.transform.TryGetComponent(out IInteractable useable))
        {
            rightEquipedInteractable = useable;
        }
    }

    public void RemoveRightInteractable(SelectExitEventArgs _args)
    {
        if (_args.interactableObject.transform.TryGetComponent(out IInteractable useable))
        {
            rightEquipedInteractable = null;
        }
    }
}
