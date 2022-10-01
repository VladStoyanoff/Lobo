using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] float spawnRate = 10;
    [SerializeField] GameObject[] enemyUnitPrefabs;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        GameManager.OnGameStarted += GameManager_OnGameStarted;
    }

    void GameManager_OnGameStarted(object sender, EventArgs e)
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        var index = UnityEngine.Random.Range(0, enemyUnitPrefabs.Length);
        while (gameManager.GetIsGameActiveBool())
        {
            Instantiate(enemyUnitPrefabs[index], transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
