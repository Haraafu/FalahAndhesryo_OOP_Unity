using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyForward : Enemy
{
    private float speed = 5f;
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

        position = new Vector2(Random.Range(leftBorder, rightBorder), topBorder);
        transform.position = position;

    }

    void Update()
    {
        position.y -= speed * Time.deltaTime;
        transform.position = position;

        if (transform.position.y <= bottomBorder)
        {
            Destroy(gameObject);
        }
    }
}