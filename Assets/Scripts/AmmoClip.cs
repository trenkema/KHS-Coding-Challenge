using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClip : MonoBehaviour, IInteractable
{
    [SerializeField] private int amountOfBullets = 12;
    [SerializeField] private WeaponType weaponType = WeaponType.Gun;

    /// <summary>
    /// Send to the equiped weapon to reload with specific amount of bullets and weapon type.
    /// </summary>
    public void PrimaryButton()
    {
        EventSystemNew<int, WeaponType>.RaiseEvent(Event_Type.RELOAD_WEAPON, amountOfBullets, weaponType);

        Destroy(gameObject);
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
