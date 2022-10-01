// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMovement : MonoBehaviour
// {
//    public float speed;
//    public Rigidbody2D rb;

//    private Vector3 movement;     //(x,y,0) vector
//    private float move_x;               //x coordinate of player
//    private float move_y;               //y coordinate of player
//     public Vector2 jump_height;
//     private float jump_speed;

//    private BoxCollider2D collider;

//    private SpriteRenderer m_sprite;

//    void Start()
//    {
//        speed = 7f;
//        movement = new Vector3(0.0f, 0.0f, 0.0f);

//        collider = GetComponent<BoxCollider2D>();
//        m_sprite = GetComponent<SpriteRenderer>();
//        jump_height = new Vector2(10,10);
//        jump_speed = 7f;
//    }

//    void Update()
//    {
//        move_x = Input.GetAxisRaw("Horizontal"); //returns a -1/0/1
//        move_y = Input.GetAxisRaw("Vertical");

//     //    movement = new Vector3(move_x, move_y, 0.0f);
//         movement = new Vector3(move_x, 0, 0.0f);
//        movement = movement.normalized;
       
//        UpdateAnimation();
//    }

//    void FixedUpdate()
//    {
//        if(movement != Vector3.zero) //if no input, then don't move
//        {
//             rb.velocity = new Vector2(move_x * speed, rb.velocity.y); //physically moves Nuvi in the direction

//         //    rb.MovePosition(transform.position + speed * movement * Time.deltaTime); //physically moves Nuvi in the direction
           
//            // transform.rotation = Quaternion.LookRotation(transform.forward, -movement); //for facing the direction its moving
//            //-movement bc otherwise it will face the opposite direction since transform is facing downwards
//        }
//        if (Input.GetKeyDown(KeyCode.Space))  //makes player jump
//         {
//             //GetComponent<Rigidbody2D>().AddForce(jump_height, ForceMode2D.Impulse);
//             rb.velocity = new Vector2(rb.velocity.x, jump_speed);
//         }
//    }

//    private void UpdateAnimation()
//    {
//         if (move_x > 0f)
//         {
//             //state = AnimationState.running;
//             m_sprite.flipX = true;
//         }
//         else if (move_x < 0f)
//         {
//             //state = AnimationState.running;
//             m_sprite.flipX = false;
//         }
        
//    }

//     // void OnCollisionEnter2D(Collision2D collision)
//     // {
//     //     if (collision.gameObject.tag == "Frame")
//     //     {
//     //         Debug.Log("entered");
//     //         //collision.gameObject.SendMessage("ApplyDamage", 10);
//     //     }
//     // }
// }

    // private void Flip()
    // {
    //     if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
    //     {
    //         isFacingRight = !isFacingRight;
    //         Vector3 localScale = transform.localScale;
    //         localScale.x *= -1f;
    //         transform.localScale = localScale;
    //     }
    // }

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

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Animator anim;
    private enum AnimationState {boyIdle, boyRun};

    void Start()
    {
        speed = 6f;
        jumpingPower = 8f;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        is_grounded = false;
    }

    void Update()
    {
        move_x = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && is_grounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        UpdateSprite();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(move_x * speed, rb.velocity.y);
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
            sprite.flipX = true;
        }
        else if (move_x < 0f)
        {
            //state = AnimationState.running;
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

}