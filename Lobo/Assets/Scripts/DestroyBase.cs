using UnityEngine;

public class DestroyBase : MonoBehaviour
{
    [SerializeField] GameObject enemyBase;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Bullet"))
        {
            Destroy(enemyBase);
            FindObjectOfType<Spawner>().GetEnemyBases().Remove(enemyBase);
            FindObjectOfType<FuelTank>().RefillTank();
            FindObjectOfType<ScoreManager>().ModifyScore(100);
        }
    }
}
