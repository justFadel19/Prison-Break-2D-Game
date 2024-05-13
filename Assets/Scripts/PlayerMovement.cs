using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] int maxJumps = 2;
    [SerializeField] int jumpsLeft;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2 (10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float gravityScaleAtTheStart;
    Vector2 moveInput;  //for the (x, y) axis.
    Rigidbody2D playerRigidbody;
    Animator playerAnimator;
    CapsuleCollider2D playerBodyCollider;
    BoxCollider2D playerFeetCollider;

    bool isAlive = true;

    AudioManager audioManager; 


    void Start()
    {
        //access the rigidbody component to control our player movement.
        playerRigidbody = GetComponent<Rigidbody2D>();

        //access the animator component to change the animation states.
        playerAnimator = GetComponent<Animator>();

        //access the player's collider.
        playerBodyCollider = GetComponent<CapsuleCollider2D>();
        playerFeetCollider = GetComponent<BoxCollider2D>();

        gravityScaleAtTheStart = playerRigidbody.gravityScale;

        jumpsLeft = maxJumps;
    }

    void Update()
    {
        if(!isAlive) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if(!isAlive) { return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }

    //to get the input value
    void OnMove(InputValue value)
    {
        if(!isAlive) { return; }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if(!isAlive) { return; }
        if (playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            jumpsLeft = maxJumps;
        }
        if (value.isPressed && jumpsLeft > 0)
        {
            if (jumpsLeft == 1)
            {
                playerAnimator.SetBool("isDoubleJumping", true);
            }
            playerRigidbody.velocity += new Vector2(0f, jumpSpeed);
            jumpsLeft--;
        }
    }

    void Run()
    {
        //velocity is how we are going to move our character.
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, playerRigidbody.velocity.y);//access only the x axis and keeping the current velocity on y as it is.
        playerRigidbody.velocity = playerVelocity;

        //absolute returns a posistive value.
        bool playertHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;
        playerAnimator.SetBool("isRunning", playertHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(playerRigidbody.velocity.x) > Mathf.Epsilon;

        //only flipSprite if the player moving left or right.
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!playerFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            playerRigidbody.gravityScale = gravityScaleAtTheStart;
            playerAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(playerRigidbody.velocity.x, moveInput.y * climbSpeed);
        playerRigidbody.velocity = climbVelocity;

        playerRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(playerRigidbody.velocity.y) > Mathf.Epsilon;
        playerAnimator.SetBool("isClimbing", playerHasVerticalSpeed);

    }

    //added this method to the end of (DoubleJumping) animation as [add event]
    void ResetDoubleJumping()
    {
        playerAnimator.SetBool("isDoubleJumping", false);
    }

    void Die()
    {
        if(playerBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            playerAnimator.SetTrigger("Dying");
            playerRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
            // if(audioManager != null)
            // {
            //     audioManager.PlayGameOverSFX();
            // }
        }
    }
}
