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
        // Jika objek yang terkena collider memiliki tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            // Hapus senjata lama jika ada
            if (currentWeapon != null)
            {
                TurnVisual(false, currentWeapon);
            }

            // Set senjata ini sebagai senjata baru yang dipegang pemain
            currentWeapon = weapon;
            weapon.transform.SetParent(other.transform);
            weapon.transform.localPosition = new Vector3(0, 0, 1);

            // Mengaktifkan visual senjata setelah diambil
            TurnVisual(true);

            Debug.Log("Player successly picked up weapon");
        }
    }

    void TurnVisual(bool on)
    {
        // Mengaktifkan atau menonaktifkan semua komponen visual dari weapon
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    void TurnVisual(bool on, Weapon weapon)
    {
        // Overload untuk TurnVisual, memungkinkan pengaktifan atau penonaktifan objek weapon tertentu
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }
}
