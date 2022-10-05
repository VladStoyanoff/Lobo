using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float explosionRadius = .05f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Enemy Bullet") && collision.CompareTag("Enemy")) return;

        var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var col in colliders)
        {
            if (gameObject.CompareTag("Enemy Bullet") == false && col.CompareTag("Wall") == false)
            {
                Destroy(col.gameObject);
            }
        }

        if (gameObject.CompareTag("Player Bullet") && collision.CompareTag("Enemy"))
        {
            FindObjectOfType<ScoreManager>().ModifyScore(2);
        }
        Destroy(gameObject);
    }
}
