using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    public float moveSpeed;

    private Vector2 initialEnemyPosition;  // Store initial position for this enemy
    private static List<EnemyMovement> enemies = new List<EnemyMovement>();  // Store all enemies
    private bool isDead = false;  // Track if the enemy is dead

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        initialEnemyPosition = transform.position;  // Store initial position
        enemies.Add(this);  // Add this enemy to the list of all enemies
    }

    void Update()
    {
        if (!isDead)
        {
            // Constantly move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // Handle collision with the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Assuming your player has the "Player" tag
        {
            // Call PlayerDeath method from PlayerMovement
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.PlayerDeath();  // Kill the player
            }
        }
    }

    // Kill this enemy (make them disappear)
    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);  // Hide the enemy
    }

    // Reset all enemies to their initial positions
    public static void ResetAllEnemies()
    {
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.isDead = false;
            enemy.gameObject.SetActive(true);  // Reactivate enemy
            enemy.transform.position = enemy.initialEnemyPosition;  // Reset to initial position
        }
    }
}
