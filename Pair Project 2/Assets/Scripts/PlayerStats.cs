using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    float currentHealth;
    float currentRegen;
    float currentMoveSpeed;
    float currentMight;
    float currentProjectileSpeed;

    [Header("I-frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    void Update() {
        if(invincibilityTimer > 0) {
            invincibilityTimer -= Time.deltaTime;
        } else if(isInvincible) { 
            isInvincible = false; 
        }
    }

    void Awake() {
        currentHealth = characterData.MaxHealth;
        currentRegen = characterData.Regeneration;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }

    public void TakeDamage(float dmg) {
        if(!isInvincible){
            currentHealth -= dmg; 

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if(currentHealth <= 0){ 
                Kill(); 
            }
        }
        
    }

    public void Kill() {
        Debug.Log("Player died");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestoreHealth(float amount) {
        if(currentHealth < characterData.MaxHealth) {
            currentHealth += amount;
            if(currentHealth > characterData.MaxHealth) {
                currentHealth = characterData.MaxHealth;
            }
        }
    }
}

