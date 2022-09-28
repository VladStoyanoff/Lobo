using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Refactor: Turn into a switch statement
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player Bullet"))
        {
            if (collision.CompareTag("Building Block"))
            {
                // FIX: Add explosion radius
                Destroy(collision.gameObject);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
