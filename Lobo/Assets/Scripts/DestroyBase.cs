using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBase : MonoBehaviour
{
    [SerializeField] GameObject enemyBase;
    Spawner spawner;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Bullet"))
        {
            Destroy(enemyBase);
            spawner.GetEnemyBases().Remove(enemyBase);
        }
    }


}
