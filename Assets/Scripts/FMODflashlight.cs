using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMODflashlight : MonoBehaviour
{

    private FMOD.Studio.EventInstance ButtonActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ButtonActive = FMODUnity.RuntimeManager.CreateInstance("event:/Button_On");
        ButtonActive.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        ButtonActive.start();
    }
}
