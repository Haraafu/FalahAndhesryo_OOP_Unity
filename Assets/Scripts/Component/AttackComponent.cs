using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private float damage;
    private HitboxComponent hitboxComponent;
    private InvicibilityComponent invicibilityComponent;

    void Awake()
    {
        bullet = GetComponent<Bullet>();
        damage = bullet.damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {   
        if (other.gameObject.CompareTag(gameObject.tag))
        {
            return;
        }

        invicibilityComponent = other.GetComponent<InvicibilityComponent>();
        hitboxComponent = other.GetComponent<HitboxComponent>();
        
        if (hitboxComponent != null)
        {
            if (invicibilityComponent != null)
            {
                hitboxComponent.Damage(damage);
                invicibilityComponent.Flashing();
            } 
        }
    }
}
