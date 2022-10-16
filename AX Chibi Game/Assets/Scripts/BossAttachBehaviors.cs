using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttachBehaviors : MonoBehaviour
{
    [SerializeField] private int typeOfAttack;
    [SerializeField] private Rigidbody2D rb;
    private SpriteRenderer spriteRender;
    private float speed;
    private int LoR;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(3, 9); //ignore ground and Bossattacks layer
        Physics2D.IgnoreLayerCollision(6, 9); //ignore frame and BossAttacks layer
        // Physics2D.IgnoreLayerCollision(7, 9); //ignore player and Bossattacks layer
        switch (typeOfAttack)
        {
            case 1:
                SlashStart();
                break;
            case 2:
                StabStart();
                break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (typeOfAttack)
        {
            case 1:
                rb.velocity = new Vector2(LoR * speed, rb.velocity.y);
                break;
        }
    }

    void SlashStart()
    {
        speed = 6f;
        if (transform.position.x > 0)
        {
            LoR = -1;
            spriteRender = this.GetComponent<SpriteRenderer>();
            spriteRender.flipX = true;
        }
        else
        {
            LoR = 1;

        }
    }

    void StabStart()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(WarningOver());
    }

    private IEnumerator WarningOver()
    {
        yield return new WaitForSeconds(0.75f);
        this.transform.GetChild(1).gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject.transform.parent.gameObject);
    }

}
