using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    public float moveSpeed;

    private Vector2 initialPlayerPosition;  // Store initial player position
    private static List<EnemyMovement> enemies = new List<EnemyMovement>();  // Store all enemies
    private Vector2 initialEnemyPosition;  // Store initial position for this enemy

    private Rigidbody2D rb;

    void Start()
    {
        // Get player reference and Rigidbody
        player = FindObjectOfType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();

        // Ensure Rigidbody is Kinematic and collision detection is continuous
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        // Store initial position for this enemy
        initialEnemyPosition = transform.position;

        // Store the initial player position (optional if player respawns to the same place)
        initialPlayerPosition = player.position;

        // Add this enemy to the list of all enemies
        enemies.Add(this);
    }

    void Update()
    {
        // Constantly move towards player
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    // When the enemy touches the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the enemy! Resetting all positions...");

            // Reset player position
            other.transform.position = initialPlayerPosition;

            // Reset all enemy positions
            ResetAllEnemies();
        }
    }

    // Reset all enemies to their initial positions
    private void ResetAllEnemies()
    {
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.transform.position = enemy.initialEnemyPosition;  // Reset each enemy to its initial position
        }
    }
}
