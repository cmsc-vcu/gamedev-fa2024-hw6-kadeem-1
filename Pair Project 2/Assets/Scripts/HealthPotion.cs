using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int healthRestore; 

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Player")){
            PlayerStats player = col.GetComponent<PlayerStats>();
            if(player != null){
                player.RestoreHealth(healthRestore);
                Destroy(gameObject);
            }
        }
    }
}
