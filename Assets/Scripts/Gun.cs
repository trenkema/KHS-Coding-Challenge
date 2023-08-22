using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMOD.Studio;

public enum FireMode
{
    Single,
    Automatic
}

public enum WeaponType
{
    Gun
}

public class Gun : MonoBehaviour, IInteractable
{
    [Header("Weapon Settings")]
    [SerializeField] private int maxAmmoCount = 12;
    [SerializeField] private float timeBetweenFiringSingleMode = 0.5f;
    [SerializeField] private float timeBetweenFiringAutomaticMode = 0.15f;
    [SerializeField] private float firePower = 350f;
    [SerializeField] private WeaponType weaponType = WeaponType.Gun;

    [Header("References")]
    [SerializeField] private TextMeshPro ammoCountText;
    [SerializeField] private TextMeshPro fireModeText;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private Transform firePosition;

    private FireMode currentFireMode = FireMode.Single;
    private int currentAmmoCount = 12;
    private bool triggerReleased = true;
    private bool isEquiped = false;
    private bool canHoldTrigger = false;
    private float currentTimeBetweenFiring = 0.5f;
    private bool holdingTrigger = false;
    public float fireCooldownTimer = 0f;
    private FMOD.Studio.EventInstance GunShotSound;
    private FMOD.Studio.EventInstance ReloadSound;
    private void OnEnable()
    {
        EventSystemNew<int, WeaponType>.Subscribe(Event_Type.RELOAD_WEAPON, ReloadWeapon);
    }

    private void OnDisable()
    {
        EventSystemNew<int, WeaponType>.Unsubscribe(Event_Type.RELOAD_WEAPON, ReloadWeapon);
    }

    /// <summary>
    /// Set the cooldown between firing depending on the fire mode.
    /// Set the fire mode text depending on the fire mode.
    /// </summary>
    private void Start()
    {
        switch (currentFireMode)
        {
            case FireMode.Single:
                canHoldTrigger = false;
                currentTimeBetweenFiring = timeBetweenFiringSingleMode;
                fireModeText.text = "Single";
                break;
            case FireMode.Automatic:
                canHoldTrigger = true;
                currentTimeBetweenFiring = timeBetweenFiringAutomaticMode;
                fireModeText.text = "Automatic";
                break;
        }
    }

    /// <summary>
    /// Reduce the cooldown to fire.
    /// Fire when cooldown is done and having ammo.
    /// Set cooldown depending on fire mode after firing.
    /// </summary>
    private void Update()
    {
        fireCooldownTimer -= Time.deltaTime;

        if (holdingTrigger && isEquiped)
        {
            if (canHoldTrigger || !canHoldTrigger && triggerReleased)
            {
                if (fireCooldownTimer <= 0 && currentAmmoCount > 0)
                {
                    Fire();
                    triggerReleased = false;
                    fireCooldownTimer = currentTimeBetweenFiring;
                }
            }
        }
    }

    /// <summary>
    /// Change the fire mode, time it takes to fire every bullet and the fire mode text.
    /// </summary>
    public void PrimaryButton()
    {
        currentFireMode++;

        if ((int)currentFireMode > System.Enum.GetValues(typeof(FireMode)).Length - 1)
        {
            currentFireMode = 0;
        }

        switch (currentFireMode)
        {
            case FireMode.Single:
                canHoldTrigger = false;
                currentTimeBetweenFiring = timeBetweenFiringSingleMode;
                fireModeText.text = "Single";
                break;
            case FireMode.Automatic:
                canHoldTrigger = true;
                currentTimeBetweenFiring = timeBetweenFiringAutomaticMode;
                fireModeText.text = "Automatic";
                break;
        }
    }

    public void SecondaryButton()
    {
    }

    /// <summary>
    /// Disallow the weapon to fire and reset the trigger.
    /// </summary>
    public void TriggerCanceled()
    {
        triggerReleased = true;
        holdingTrigger = false;
    }

    /// <summary>
    /// Allow the weapon to fire.
    /// </summary>
    public void TriggerStarted()
    {
        holdingTrigger = true;
    }

    /// <summary>
    /// Allow the weapon to be reloaded and used.
    /// </summary>
    public void GunEquiped()
    {
        isEquiped = true;
    }

    /// <summary>
    /// Reset all the variables required for firing.
    /// </summary>
    public void GunDropped()
    {
        isEquiped = false;
        triggerReleased = true;
        holdingTrigger = false;
    }

    /// <summary>
    /// Reduce ammo, update the ammo text and spawn a bullet with force.
    /// </summary>
    private void Fire()
    {
        currentAmmoCount--;

        ammoCountText.text = currentAmmoCount.ToString();

        Instantiate(ammoPrefab, firePosition.position, firePosition.rotation).GetComponent<Rigidbody>().AddForce(firePosition.forward * firePower);

        GunShotSound = FMODUnity.RuntimeManager.CreateInstance("event:/shot");
        GunShotSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        GunShotSound.start();
    }

    /// <summary>
    /// Reload the weapon if compatible with the ammo.
    /// </summary>
    /// <param name="_ammoAmount">Amount of ammo to reload</param>
    /// <param name="_weaponType">Weapon type to check compatible ammo</param>
    private void ReloadWeapon(int _ammoAmount, WeaponType _weaponType)
    {
        if (_weaponType == weaponType && isEquiped)
        {
            currentAmmoCount = _ammoAmount;
            ammoCountText.text = currentAmmoCount.ToString();
            fireCooldownTimer = 0f;

        ReloadSound = FMODUnity.RuntimeManager.CreateInstance("event:/Reload");
        ReloadSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        ReloadSound.start();
        }
    }
}
