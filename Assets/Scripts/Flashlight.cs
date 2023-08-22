using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
public class Flashlight : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject lightSource;
    [SerializeField] private GameObject frontLightIndicator;
    private FMOD.Studio.EventInstance FlashSoundOn;
    private FMOD.Studio.EventInstance FlashSoundOff;
    private FMOD.Studio.EventInstance FlashSoundActive;
    /// <summary>
    /// Enable/Disable the light and light indicator panel.
    /// </summary>
    public void PrimaryButton()
    {
        lightSource.SetActive(!lightSource.activeInHierarchy);
        frontLightIndicator.SetActive(!frontLightIndicator.activeInHierarchy);

       FlashSoundOn = FMODUnity.RuntimeManager.CreateInstance("event:/Flashlight_On");
       FlashSoundOn.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
       FlashSoundOn.start();
       { 
        if (lightSource.activeInHierarchy == true)
            {
            FlashSoundActive = FMODUnity.RuntimeManager.CreateInstance("event:/Flashlight_active");
            FlashSoundActive.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            FlashSoundActive.start();
            }
            else if(lightSource.activeInHierarchy == false)
            {
                 FlashSoundActive.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                 FlashSoundActive.release();
             }
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