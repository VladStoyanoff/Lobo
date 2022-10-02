using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float explosionRadius = .05f;

    ScoreManager scoreManager;

    // Refactor: Turn into a switch statement

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        if (gameObject.CompareTag("Player Bullet"))
        {
            if (collision.CompareTag("Building Block"))
            {
                foreach(var col in colliders)
                {
                    Destroy(col.gameObject);
                }

            }

            else if (collision.CompareTag("Enemy"))
            {
                foreach (var col in colliders)
                {
                    scoreManager.ModifyScore(2);
                    Destroy(col.gameObject);
                }
            }
            foreach (var col in colliders)
            {
                scoreManager.ModifyScore(2);
                Destroy(col.gameObject);
            }
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Enemy Bullet"))
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Enemy") == false)
            {
                Destroy(gameObject);
            }
        }
    }
}
