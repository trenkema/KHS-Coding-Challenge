using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour
{
    [SerializeField] private UnityEvent onPressed;
    [SerializeField] private UnityEvent onReleased;

    /// <summary>
    /// Invoke an event when a Button enters the trigger.
    /// </summary>
    /// <param name="other">The collider this object collides with</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            onPressed?.Invoke();
        }
    }

    /// <summary>
    /// Invoke an event when a Button exists the trigger.
    /// </summary>
    /// <param name="other">The collider this object collides with</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Button"))
        {
            onReleased?.Invoke();
        }
    }
}
