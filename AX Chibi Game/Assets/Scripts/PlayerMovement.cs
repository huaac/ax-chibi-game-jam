using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float move_x;
    private float speed;
    private float jumpingPower;
    private SpriteRenderer sprite;
    private bool is_grounded;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = .5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Animator anim;
    private enum AnimationState {boyIdle, boyRun};

    public ParticleSystem dust;

    void Start()
    {
        speed = 6f;
        jumpingPower = 8f;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        is_grounded = false;

        activeMoveSpeed = speed;
    }

    void Update()
    {
        move_x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            CreateDust();
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            CreateDust();
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = speed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }

        UpdateSprite();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move_x * activeMoveSpeed, rb.velocity.y);
    }

    private void IsGrounded()
    {
        // return Physics2D.OverlapCircle(groundCheck.position, 0.02f, groundLayer);
        is_grounded = true;
        
    }

    private void IsNotGrounded()
    {
        // return Physics2D.OverlapCircle(groundCheck.position, 0.02f, groundLayer);
        is_grounded = false;
        
    }

    private void UpdateSprite()
    {
        if (move_x > 0f)
        {
            //state = AnimationState.running;
            CreateDust();
            sprite.flipX = true;
        }
        else if (move_x < 0f)
        {
            //state = AnimationState.running;
            CreateDust();
            sprite.flipX = false;
        }
    }

    private void UpdateAnimation()
    {
        AnimationState state;

        if (move_x != 0f) {state = AnimationState.boyRun;}
        else {state = AnimationState.boyIdle;}

        anim.SetInteger("currentState", (int)state);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            IsGrounded();
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            IsNotGrounded();
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

}