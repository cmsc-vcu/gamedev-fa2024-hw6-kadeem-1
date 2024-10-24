using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    public float moveSpeed;
    
    // Health-related variables
    public int enemyHealth = 60; // Start health at 60 points
    public int damagePerHit = 10; // Damage per hit from the knife
    
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

    // Handle collision with the knife
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knife Weapon"))  // Assuming the knife has the "Knife" tag
        {
            TakeDamage(damagePerHit);  // Call TakeDamage when hit by the knife
        }

        if (other.CompareTag("Player"))  // Assuming the player has the "Player" tag
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.PlayerDeath();  // Kill the player
            }
        }
    }

    // Method to reduce enemy health
    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;  // Reduce health

        if (enemyHealth <= 0)
        {
            Die();  // Kill the enemy if health reaches zero
        }
    }

    // Kill the enemy (make them disappear)
    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);  // Hide the enemy
    }

    // Method to increase enemy speed (for use with the timer)
    public void IncreaseSpeed(float increment)
    {
        moveSpeed += increment;  // Increase the enemy's movement speed
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
