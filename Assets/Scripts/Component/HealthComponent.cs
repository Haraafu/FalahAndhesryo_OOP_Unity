using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    float health;

    void Awake()
    {
        health = maxHealth;
    }

    void getHealth(float health)
    {
        this.health = health;
    }

    public float Subtract(float value)
    {
        health -= value;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        return health;
    }
}
