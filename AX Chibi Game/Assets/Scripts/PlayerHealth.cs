using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private List<GameObject> _hearts;
    private int _currHeartNum;
    private bool isInvincible;
    [SerializeField] private float invincibilityDurationSeconds;
    [SerializeField] private float invincibilityDeltaTime;

    // [SerializeField] private GameObject model;
    private bool is_transparent;
    [SerializeField] private SpriteRenderer model;

    // Start is called before the first frame update
    void Start()
    {
        _currHeartNum = 3;
        Debug.Log(_currHeartNum);
        isInvincible = false;
        is_transparent = false;
    }

    void OnEnable()
    {
        print("testing to see if enabling works");
        _currHeartNum = 3;
        Debug.Log(_currHeartNum);
        isInvincible = false;
        is_transparent = false;
        for(int i = 0; i < 3; i++)
        {
            _hearts[i].SetActive(true);
        }
    }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        Debug.Log("Player turned invincible!");
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(7, 8, true); //ignore player and enemy layer
        Physics2D.IgnoreLayerCollision(7, 9, true); //ignore player and Bossattacks layer
        
        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (is_transparent == false)
            {
                // if not transparent, set transparent
                model.color = new Color(1f,1f,1f,0f);
                is_transparent = true;
            }
            else
            {
                // if is transparent, set not transparent
                model.color = new Color(1f,1f,1f,1f);
                is_transparent = false;
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        // yield return new WaitForSeconds(invincibilityDurationSeconds);

        model.color = new Color(1f,1f,1f,1f);
        is_transparent = false;
        isInvincible = false;

        Physics2D.IgnoreLayerCollision(7, 8, false); //ignore player and enemy layer
        Physics2D.IgnoreLayerCollision(7, 9, false); //ignore player and Bossattacks layer

        Debug.Log("Player is no longer invincible!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!isInvincible)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                // When a hunter collides with a ghost, hunter's health falls to 0 and is downed
                if (_currHeartNum != 0)
                {
                    _hearts[_currHeartNum-1].SetActive(false);
                    _currHeartNum--;
                    Debug.Log("taken damage");
                    
                    StartCoroutine(BecomeTemporarilyInvincible());
                }
            }
        }
    }

    //invincibility frame visual flashing
    private void ScaleModelTo(Vector3 scale)
    {
        model.transform.localScale = scale;
    }

    public int OutOfHealth()
    {
        return _currHeartNum;
    }
}
