using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    float currentHealth;
    float currentRegen;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    // Add a field for respawn position
    public Vector2 respawnPosition;

    void Awake() {
        currentHealth = characterData.MaxHealth;
        currentRegen = characterData.Regeneration;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;

        respawnPosition = transform.position; // Set initial respawn position
    }

    public void TakeDamage(float dmg) {
        currentHealth -= dmg; 

        if(currentHealth <= 0){ 
            Respawn(); // Call respawn method instead of Kill
        }
    }

    // New method to handle respawning
    private void Respawn() {
        currentHealth = characterData.MaxHealth; // Reset health to max health
        transform.position = respawnPosition; // Respawn player at the designated position
        // Additional logic to reset the player's state can be added here if needed
    }

    public void Kill() {
        Destroy(gameObject);
    }
}

