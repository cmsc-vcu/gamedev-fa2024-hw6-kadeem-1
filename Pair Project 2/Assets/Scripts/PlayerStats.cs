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

    void Awake() {
        currentHealth = characterData.MaxHealth;
        currentRegen = characterData.Regeneration;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }

    public void TakeDamage(float dmg) {
        currentHealth -= dmg; 

        if(currentHealth <= 0){ 
            Kill();
        }
    }

    public void Kill() {
        Destroy(gameObject);
    }
}
