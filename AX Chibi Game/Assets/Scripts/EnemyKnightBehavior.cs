using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnightBehavior : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;

    private SpriteRenderer spriteRender;
    private float speed;
    private int LoR;
    //Left is positive 1, R is negative 1

    public ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        Physics2D.IgnoreLayerCollision(6, 8); //ignore frame and enemy layer
        // Physics2D.IgnoreLayerCollision(7, 8); //ignore player and enemy layer
        if(transform.position.x > 0)
        {
            LoR = -1;
        }
        else{
            LoR = 1;
            spriteRender = this.GetComponent<SpriteRenderer>();
            spriteRender.flipX = true;
        }
    }


    void FixedUpdate()
    {
        
        //change 1 to a -1 to make it go the opposite way
        rb.velocity = new Vector2(LoR * speed, rb.velocity.y);
        CreateDust();
    }

    void CreateDust()
    {
        dust.Play();
    }
}
