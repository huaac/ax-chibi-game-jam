using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnightBehavior : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        Physics2D.IgnoreLayerCollision(6, 8); //ignore frame and enemy layer
        Physics2D.IgnoreLayerCollision(7, 8); //ignore player and enemy layer
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(1 * speed, rb.velocity.y);
    }
}
