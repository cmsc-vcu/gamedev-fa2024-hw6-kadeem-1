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

    public int health = 60;  // Default health points for the enemy
    public int damagePerHit = 10;  // Amount of damage dealt to the player on contact

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        initialEnemyPosition = transform.position;  // Store initial position
        enemies.Add(this);  // Add this enemy to the list of all enemies
    }

    void Update()
    {
        if (!isDead && player != null)  // Only move if the enemy is not dead and the player still exists
        {
            // Constantly move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    // Handle collision with the knife or player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Knife Weapon"))
        {
            TakeDamage(10);  // Reduce health by 10 when hit by the knife
        }
        else if (other.CompareTag("Player"))  // Assuming your player has the "Player" tag
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.PlayerDeath();  // Kill the player
            }
        }
    }

    // Continuously deal damage to the player when in contact
    private void OnCollisionStay2D(Collision2D col)
{
    if (col.gameObject.CompareTag("Player"))
    {
        PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
        if (player != null)
        {
            player.TakeDamage(damagePerHit); // Call TakeDamage method from PlayerStats
        }
    }
}


    // Kill this enemy (make them disappear)
    public void Die()
    {
        isDead = true;
        gameObject.SetActive(false);  // Hide the enemy
    }

    public void IncreaseSpeed(float increaseAmount)
    {
        moveSpeed += increaseAmount;  // Increase the enemy's movement speed
    }

    // Enemy takes damage
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Reset all enemies to their initial positions
    public static void ResetAllEnemies()
    {
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.isDead = false;
            enemy.gameObject.SetActive(true);  // Reactivate enemy
            enemy.transform.position = enemy.initialEnemyPosition;  // Reset to initial position
            enemy.health = 60;  // Reset health to default 60
        }
    }
}
