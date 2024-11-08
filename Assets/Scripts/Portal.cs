using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private float speed; // Seberapa cepat Portal Asteroid Bergerak
    [SerializeField] private float rotateSpeed; // Seberapa cepat Portal Asteroid Berputar

    private Vector2 newPosition; // Adalah posisi yang dapat di-travel oleh asteroid

    void Start()
    {
        ChangePosition(); // Inisialisasi nilai newPosition
    }

    void Update()
    {
        // Cek apakah jarak antara posisi asteroid saat ini dengan posisi newPosition < 0.5
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        if (Player.Instance != null && WeaponPickup.currentWeapon == null)
        {
            GetComponent<Renderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
            GetComponent<Collider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.levelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // Bottom-left
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // Top-right

        float x = Random.Range(min.x, max.x);
        float y = Random.Range(min.y, max.y);
        newPosition = new Vector2(x, y);
    }
}
