using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Slash")
        {
            Destroy(collision.gameObject);
            
        }
    }
}
