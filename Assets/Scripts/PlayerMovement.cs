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

        // Calculate initial values
        moveVelocity = new Vector2(2 * (maxSpeed.x / timeToFullSpeed.x), 2 * (maxSpeed.y / timeToFullSpeed.y)
        );

        moveFriction = new Vector2(-2 * (maxSpeed.x / (timeToFullSpeed.x * timeToFullSpeed.x)), -2 * (maxSpeed.y / (timeToFullSpeed.y * timeToFullSpeed.y))
        );

        stopFriction = new Vector2(-2 * (maxSpeed.x / (timeToStop.x * timeToStop.x)), -2 * (maxSpeed.y / (timeToStop.y * timeToStop.y))
        );
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
        if (moveDirection == Vector2.zero)
        {
            return stopFriction;
        }

        if (moveDirection.x != 0 && moveDirection.y == 0)
        {
            return new Vector2(moveFriction.x, 0);
        }

        if (moveDirection.y != 0 && moveDirection.x == 0)
        {
            return new Vector2(0, moveFriction.y);
        }

        return moveFriction;
    }

    public void MoveBound()
    {
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);

        position.x = Mathf.Clamp(position.x, 0.02f, 0.95f);
        position.y = Mathf.Clamp(position.y, 0.02f, 0.95f);

        transform.position = Camera.main.ViewportToWorldPoint(position);
    }

    public bool IsMoving()
    {
        return moveDirection != Vector2.zero;
    }
}
