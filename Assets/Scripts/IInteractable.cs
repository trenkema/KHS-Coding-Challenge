using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public interface IInteractable
{
    void PrimaryButton();
    void SecondaryButton();
    void TriggerStarted();
    void TriggerCanceled();
}
