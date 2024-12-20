using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public EnemySpawner es; 

    //Current stats
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    void Awake()
    {
        //Assign the vaiables
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if(es != null) {
            es.OnEnemyKilled();
        }
        
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D col) {
        if(col.gameObject.CompareTag("Player")){
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
    }
}
