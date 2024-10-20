using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehavior : ProjectileWeaponBehavior
{
    KnifeController kc;

    protected override void Start()
    {
        base.Start();
        kc = FindObjectOfType<KnifeController>();
    }

    void Update()
    {
        transform.position += direction * kc.speed * Time.deltaTime;  // Sets movement of Knife object
    }

    // Detect collisions between the knife and enemies (zombies)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))  // Assuming your enemies have the "Enemy" tag
        {
            EnemyMovement enemy = other.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.Die();  // Kill the enemy
            }
            Destroy(gameObject);  // Destroy the knife on impact (optional)
        }
    }
}
