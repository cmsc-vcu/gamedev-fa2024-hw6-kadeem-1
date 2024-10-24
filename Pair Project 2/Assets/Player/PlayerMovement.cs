using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movement
    [HideInInspector]
    public float lastHorizontalVector;
    
    [HideInInspector]
    public float lastVerticalVector;
    
    [HideInInspector]
    public Vector2 moveDir;
    
    [HideInInspector]
    public Vector2 lastMovedVector;
    
    private Vector2 initialPlayerPosition;  // To store initial player position

    // References
    Rigidbody2D rb;
    public CharacterScriptableObject characterData; 

    // Start is called before the first frame update
    void Start()
    {
        initialPlayerPosition = transform.position; // Store initial position
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    public void ResetPlayerPosition()
    {
        transform.position = initialPlayerPosition; // Reset position to initial
    }

    // Call this method when the player "dies"
    public void PlayerDeath()
    {
        ResetPlayerPosition();  // Reset the player to the initial position
        EnemyMovement.ResetAllEnemies(); // Reset all enemies when the player dies
    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); // Last moved on x-axis
        }

        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector); // Last moved on y-axis
        }

        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); // While moving
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * characterData.MoveSpeed, moveDir.y * characterData.MoveSpeed);
    }
}
