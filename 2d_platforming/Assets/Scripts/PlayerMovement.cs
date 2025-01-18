using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public SpriteRenderer playerSprite;
    public float moveSpeed;
    public float jumpPower;
    public float input;

    public float jumpTime;
    public float jumpTimeCounter;
    private bool isJumping;

    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckRadius;

    // Update is called once per frame
    void Update()
    {
        // Movement
        input = Input.GetAxisRaw("Horizontal"); // Gets the input from the player

        if (input < 0) // Flips the player sprite when moving left or right
        {
            playerSprite.flipX = true;
        }
        else if (input > 0)
        {
            playerSprite.flipX = false;
        }

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckRadius, groundLayer); // Checks if the player is grounded


        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded) // Makes the player jump
        {
            isJumping = true;
            jumpTimeCounter = jumpTime; // Resets the jump time counter
            playerRb.velocity = Vector2.up * jumpPower;
        }

        if (Input.GetButton("Jump") && isJumping) // Makes the player jump higher if the jump button is held
        {
            if (jumpTimeCounter > 0) // Checks if the jump time counter is greater than 0
            {
                playerRb.velocity = Vector2.up * jumpPower;
                jumpTimeCounter -= Time.deltaTime; // Decreases the jump time counter
            }
            else
            {
                isJumping = false; // Stops the player from jumping after they release the button
            }
        }

        if (Input.GetButtonUp("Jump")) // Stops the player from jumping when the jump button is released
        {
            isJumping = false;
        }

    }

    void FixedUpdate() // Like update but for physics calculations
    {
        playerRb.velocity = new Vector2(input * moveSpeed, playerRb.velocity.y); // Moves the player
    }
}
