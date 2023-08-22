using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FMOD.Studio;
public class Lever : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private HingeJoint hinge;
    [SerializeField] private UnityEvent onMinLimit;
    [SerializeField] private UnityEvent onMaxLimit;

    private bool isSelected = false;
    private bool hasReachedLimit = false;

    private FMOD.Studio.EventInstance LeverSoundOn;
    private FMOD.Studio.EventInstance LeverSoundOff;
    private FMOD.Studio.EventInstance LeverSoundMove;
    /// <summary>
    /// Check for the angle of hinge when this lever is selected.
    /// </summary>
    private void Update()
    {
        if (isSelected)
        {
            CheckAngle();
            CheckAngleAudio();
            DisableAngleAudio();
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

                LeverSoundMove.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                LeverSoundMove.release();



            }
        }
        else
        {
            if (hinge.angle != hinge.limits.min || hinge.angle != hinge.limits.max)
            {
                hasReachedLimit = false;

                //LeverSoundOn.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                //LeverSoundOn.release();
                LeverSoundOn.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                LeverSoundOn.release();
                LeverSoundMove = FMODUnity.RuntimeManager.CreateInstance("event:/Lever_Move");
                LeverSoundMove.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                LeverSoundMove.start();
            }
        }
    }

    private void CheckAngleAudio()
        {
            if (hinge.angle > 80)
            {
                
                LeverSoundMove.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                LeverSoundMove.release();

                Debug.Log("The lever is ON");

                
            }
                if (hinge.angle < -80)
                    {

                    LeverSoundMove.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    LeverSoundMove.release();

                    LeverSoundOn.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    LeverSoundOn.release();

                    LeverSoundOff = FMODUnity.RuntimeManager.CreateInstance("event:/Lever_Off");
                    LeverSoundOff.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                    LeverSoundOff.start();
                    Debug.Log("The lever is OFF");

                    }
            if (hasReachedLimit = true)
            {
                LeverSoundOn = FMODUnity.RuntimeManager.CreateInstance("event:/Lever_On");
                LeverSoundOn.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                LeverSoundOn.start();
            }
            if (hasReachedLimit = false)
            {
                    LeverSoundOn.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    LeverSoundOn.release();
            }

            
        }
    private void DisableAngleAudio()
                {
                if (hinge.angle < 79)
                {
                    LeverSoundOn.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                    LeverSoundOn.release();
                }
                }

    /// <summary>
    /// Only freeze the X and Y rotation when selecting the lever.
    /// </summary>
    public void SelectLever()
    {
        isSelected = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;

                LeverSoundMove = FMODUnity.RuntimeManager.CreateInstance("event:/Lever_Move");
                LeverSoundMove.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
                LeverSoundMove.start();
    }

    /// <summary>
    /// Freeze all the constraints of the rigidbody when deselecting the lever.
    /// </summary>
    public void DeSelectLever()
    {
        isSelected = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        LeverSoundMove.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        LeverSoundMove.release();
    }

}
