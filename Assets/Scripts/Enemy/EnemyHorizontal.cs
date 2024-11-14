using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private float speed = 5f;
    private float moveDirection;
    private Vector2 position;
    float topBorder;
    float bottomBorder;
    float leftBorder;
    float rightBorder;

    void Start()
    {
        topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

        // Using float range and checking condition accordingly
        if (Random.Range(0f, 1f) > 0.5f)
        {
            moveDirection = 1;
        }
        else
        {
            moveDirection = -1;
        }

        if (moveDirection == 1)
        {
            position = new Vector2(leftBorder, Random.Range(bottomBorder, topBorder));
        }
        else
        {
            position = new Vector2(rightBorder, Random.Range(bottomBorder, topBorder));
        }

        transform.position = position; // Initially set the position
    }

    void Update()
    {
        position.x += moveDirection * speed * Time.deltaTime;
        transform.position = position; // Apply position change

        // Check for boundaries and reverse direction if necessary
        if (transform.position.x >= rightBorder || transform.position.x <= leftBorder)
        {
            moveDirection *= -1; // Flip the movement direction
        }
    }
}
