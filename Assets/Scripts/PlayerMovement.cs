using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 maxSpeed;
    [SerializeField] Vector2 timeToFullSpeed;
    [SerializeField] Vector2 timeToStop;
    [SerializeField] Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * (maxSpeed / timeToFullSpeed);
        moveFriction = -2 * (maxSpeed / Mathf.Pow(timeToFullSpeed.x, 2));
        stopFriction = -2 * (maxSpeed / Mathf.Pow(timeToStop.x, 2));
    }

    public void Move()
    {
        // Ambil input secara horizontal dan vertikal
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveDirection = new Vector2(x, y).normalized;

        // Hitung kecepatan baru
        Vector2 newVelocity = (moveDirection * moveVelocity) - (GetFriction() * Time.fixedDeltaTime);

        // Batasi kecepatan maksimum
        newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed.x, maxSpeed.x);
        newVelocity.y = Mathf.Clamp(newVelocity.y, -maxSpeed.y, maxSpeed.y);

        // Terapkan kecepatan ke Rigidbody2D
        rb.velocity = newVelocity;

        // Hentikan player jika kecepatan di bawah stopClamp
        if (Mathf.Abs(rb.velocity.x) < stopClamp.x && Mathf.Abs(rb.velocity.y) < stopClamp.y)
        {
            rb.velocity = Vector2.zero;
            moveDirection = Vector2.zero;
        }
    }
    
    public Vector2 GetFriction()
    {
        return moveDirection == Vector2.zero ? stopFriction : moveFriction;
    }

    public void MoveBound()
    {
        // Implement boundary check if needed
    }

    public bool IsMoving()
    {
        return moveDirection != Vector2.zero;
    }
}
