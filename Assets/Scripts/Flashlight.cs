using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject lightSource;
    [SerializeField] private GameObject frontLightIndicator;

    /// <summary>
    /// Enable/Disable the light and light indicator panel.
    /// </summary>
    public void PrimaryButton()
    {
        lightSource.SetActive(!lightSource.activeInHierarchy);
        frontLightIndicator.SetActive(!frontLightIndicator.activeInHierarchy);
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
