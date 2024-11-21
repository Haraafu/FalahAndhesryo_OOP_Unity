using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponHolder;  // Senjata yang akan diambil
    private Weapon weapon;

    // Variabel untuk menyimpan senjata lama
    public static Weapon currentWeapon { get; private set; }

    void Awake()
    {
        // Menginisialisasi senjata baru
        weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (currentWeapon != null)
            {
                TurnVisual(false, currentWeapon);
            }

            currentWeapon = weapon;
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = new Vector3(0, 0, 1);

            TurnVisual(true);
        }
    }

    void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}