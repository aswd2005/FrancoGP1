using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    public Transform callingDisck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public int maxJumpCount;
    public int jumpCount;
    private bool isJumping;
    private bool isGrounded;

    private void Start()
    {
        jumpCount = maxJumpCount;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ProcessInputs();
        
        Animate();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, .2f, groundObjects);
        if(isGrounded)
        {
            jumpCount = maxJumpCount;
        }
        Move();
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if(isJumping && jumpCount > 0)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            jumpCount--;
        }
        isJumping = false;
        float timeReset = Time.time;
        if (timeReset >= timeReset + 5)
        {
            maxJumpCount = 1;
        }
    }

    // Update is called once per frame
    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && !facingRight)
        {
            FlipCharacter();
        }
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
    }
    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            isJumping = true;
        }
    }
    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    IEnumerator PowerUpSpeed()
    {
        moveSpeed = 9;
        yield return new WaitForSeconds(5);
        moveSpeed = 5;
    }
    public void SpeedPowerUp()
    {
        StartCoroutine(PowerUpSpeed());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
}
