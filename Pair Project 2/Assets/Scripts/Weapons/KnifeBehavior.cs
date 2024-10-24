using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehavior : MonoBehaviour
{
    public float knifeSpeed = 10f;
    private Vector2 direction;

    public void DirectionChecker(Vector2 moveDir)
    {
        direction = moveDir;
    }

    void Update()
    {
        // Move the knife in the set direction
        transform.Translate(direction * knifeSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the knife hits an enemy
        if (collision.CompareTag("Enemy"))
        {
            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
            if (enemy != null)
            {
                enemy.Die(); // Kill the enemy
            }
            Destroy(gameObject); // Destroy the knife after hitting something
        }
    }
}
