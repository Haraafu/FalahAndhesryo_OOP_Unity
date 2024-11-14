using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    public float speed = 5f;
    private float moveDirection;
    private float topBorder;
    private float bottomBorder;
    private float leftBorder;
    private float rightBorder;
    public Weapon weapon; // asumsikan ada class yang bernama Weapon untuk menembak
    void Start()
    {
        Camera mainCamera = Camera.main;
        topBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        bottomBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        leftBorder = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        rightBorder = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        if (Random.Range(0f, 1f) > 0.5f)
        {
            moveDirection = 1;
            transform.position = new Vector2(leftBorder, Random.Range(bottomBorder, topBorder));
        }
        else
        {
            moveDirection = -1;
            transform.position = new Vector2(rightBorder, Random.Range(bottomBorder, topBorder));
        }
    }

    void Update()
    {
        transform.position += new Vector3(moveDirection * speed * Time.deltaTime, 0, 0);

        if (transform.position.x >= rightBorder)
        {
            moveDirection = -1;
        }
        else if (transform.position.x <= leftBorder)
        {
            moveDirection = 1;
        }

        // Menembak secara periodik
        // if (weapon != null)
        // {
        //     weapon.FixedUpdate();
        // }
    }
}

