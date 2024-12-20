using UnityEngine;
using UnityEngine.SceneManagement; // For reloading the scene

public class EnemyCollision : MonoBehaviour
{
    public GameObject player; 
    // When something enters the enemy's trigger collider
    private void OnTriggerEnter2D(Collider2D col)
    {
        // Check if the object that entered the trigger is the player
        if (col.CompareTag("Player"))
        {
            // Option 1: Reload the current scene to reset everything
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Option 2: Instead of reloading, reset the player’s position manually
            // col.transform.position = new Vector3(0, 1, 0); // Example reset position
        }
    }
}


