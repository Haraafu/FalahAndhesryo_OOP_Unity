using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;
    [SerializeField] private InvicibilityComponent invicibilityComponent;
    private Bullet bullet;

    void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        invicibilityComponent = GetComponent<InvicibilityComponent>();
        bullet = GetComponent<Bullet>();
    }

    public void Damage(float damage)
    {
        if (invicibilityComponent != null && invicibilityComponent.isInvincible == false)
        {
            healthComponent.Subtract(damage);
            invicibilityComponent.Flashing();
        }
    }

    public void Damage(float damage, GameObject target)
    {
        if (invicibilityComponent != null && invicibilityComponent.isInvincible == false)
        {
            healthComponent.Subtract(bullet.damage);
            invicibilityComponent.Flashing();
        }
    }
}