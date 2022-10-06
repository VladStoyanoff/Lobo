using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float explosionRadius = .07f;
    ScoreManager scoreManager;
    AudioManager audioManager;
    int scoreForBasicUnit = 2;
    int scoreForChasePlayerUnit = 4;
    int scoreForRamPlayerUnit = 6;

    void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var col in colliders)
        {
            if (gameObject.CompareTag("Enemy Bullet") && col.CompareTag("Building Block")) continue;
            if (col.CompareTag("Wall")) continue;
            if (col.CompareTag("Player")) continue;
            audioManager.PlayHitBlockClip();
            Destroy(col.gameObject);
        }

        if (gameObject.CompareTag("Player Bullet") && collision.gameObject.layer == 11)
        {
            audioManager.PlayDestroyedEnemyClip();

            switch (collision.tag)
            {
                case "BasicUnit":
                    {
                        scoreManager.ModifyScore(scoreForBasicUnit);
                        break;
                    }
                case "ChasePlayerUnit":
                    {
                        scoreManager.ModifyScore(scoreForChasePlayerUnit);
                        break;
                    }
                case "RamPlayerUnit":
                    {
                        scoreManager.ModifyScore(scoreForRamPlayerUnit);
                        break;
                    }
            }
        }
        Destroy(gameObject);
    }
}
