using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;

    private IObjectPool<Bullet> objectPool;
    public IObjectPool<Bullet> ObjectPool { set => objectPool = value; }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
        
        CheckBounds();
    }

    public void Deactivate()
    {
        StartCoroutine(DeactivateRoutine(5f));
    }

    IEnumerator DeactivateRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector2.zero; // Corrects to use Vector2 for Rigidbody2D
        rb.angularVelocity = 0; // Angular velocity is a float, not a Vector3

        objectPool.Release(this); // Make sure objectPool is defined and accessible
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Deactivate();
    }

    void CheckBounds()
    {
        Vector2 ppos = Camera.main.WorldToViewportPoint(transform.position);

        if (ppos.y >= 1.01f || ppos.y <= -0.01f && objectPool != null)
        {
            objectPool.Release(this);
        }
    }
}
