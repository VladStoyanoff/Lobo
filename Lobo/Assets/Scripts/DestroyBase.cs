using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBase : MonoBehaviour
{
    [SerializeField] GameObject enemyBase;
    Spawner spawner;
    FuelTank fuelTank;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        fuelTank = FindObjectOfType<FuelTank>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player Bullet"))
        {
            Destroy(enemyBase);
            spawner.GetEnemyBases().Remove(enemyBase);
            fuelTank.RefillTank();
        }
    }
}
