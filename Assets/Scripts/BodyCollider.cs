using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class BodyCollider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private XROrigin xrRig;
    [SerializeField] private CharacterController controller;

    /// <summary>
    /// Set the center of the CharacterController based on the camera of the VR player.
    /// Set the height of the CharacterController based on the height of the VR camera.
    /// Slightly move the CharacterController every frame to update the camera when moving in real space.
    /// </summary>
    private void Update()
    {
        if (xrRig.CameraInOriginSpacePos != null)
        {
            Vector3 center = xrRig.CameraInOriginSpacePos;

            controller.center = new Vector3(center.x, controller.height / 2 + controller.skinWidth, center.z);

            controller.height = xrRig.CameraInOriginSpaceHeight;

            controller.Move(new Vector3(0.001f, -0.001f, 0.001f));
            controller.Move(new Vector3(-0.001f, -0.001f, -0.001f));
        }
    }
}
