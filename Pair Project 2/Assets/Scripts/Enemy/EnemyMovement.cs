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

    public float health = 60f;  // Set initial health to 60 points
    public float damagePerHit = 10f;  // Damage the enemy takes per hit

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

    // Handle collision with the knife
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knife Weapon"))  // Knife must be tagged as "Knife"
        {
            TakeDamage(damagePerHit);  // Apply damage when hit by the knife
        }

        if (other.CompareTag("Player"))  // Player must be tagged as "Player"
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.PlayerDeath();  // Kill the player
            }
        }
    }

    // Method to reduce enemy health and check for death
    public void TakeDamage(float damage)
    {
        health -= damage;  // Reduce the health by the amount of damage

        if (health <= 0)
        {
            Die();  // Call the Die method if health is 0 or below
        }
    }

    // Kill this enemy (make them disappear)
    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);  // Hide the enemy
    }

    // Increase the enemy's speed
    public void IncreaseSpeed(float increaseAmount)
    {
        moveSpeed += increaseAmount;  // Increase the enemy's movement speed
    }

    // Reset all enemies to their initial positions
    public static void ResetAllEnemies()
    {
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.isDead = false;
            enemy.health = 60f;  // Reset health to 60
            enemy.gameObject.SetActive(true);  // Reactivate enemy
            enemy.transform.position = enemy.initialEnemyPosition;  // Reset to initial position
        }
    }
}
