using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    GameManager gameManager;
    UILevelManager uiLevelManager;

    [SerializeField] float spawnRate = 10;
    [SerializeField] GameObject[] enemyUnitPrefabs;
    int index;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        uiLevelManager = FindObjectOfType<UILevelManager>();
    }

    void Start()
    {
        
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        if (uiLevelManager.GetLevelSetting() == 1)
        {
            index = 0;
        }
        else if (uiLevelManager.GetLevelSetting() == 2)
        {
            var randomModifier = UnityEngine.Random.Range(0, 100);
            if (randomModifier < 80)
            {
                index = 0;
            }
            else index = 1;
        }
        else if (uiLevelManager.GetLevelSetting() == 3)
        {
            var randomModifier = UnityEngine.Random.Range(0, 100);
            if (randomModifier < 70)
            {
                index = 0;
            }
            else if (randomModifier > 70 && randomModifier < 90)
            {
                index = 1;
            }
            else index = 2;
        }

        else if (uiLevelManager.GetLevelSetting() == 4)
        {
            var randomModifier = UnityEngine.Random.Range(0, 100);
            if (randomModifier < 50)
            {
                index = 0;
            }
            else if (randomModifier > 50 && randomModifier < 75)
            {
                index = 1;
            }
            else index = 2;
        }
        else if (uiLevelManager.GetLevelSetting() == 5)
        {
            var randomModifier = UnityEngine.Random.Range(0, 100);
            if (randomModifier < 40)
            {
                index = 0;
            }
            else if (randomModifier > 40 && randomModifier < 70)
            {
                index = 1;
            }
            else index = 2;
        }
        else if (uiLevelManager.GetLevelSetting() == 6)
        {
            var randomModifier = UnityEngine.Random.Range(0, 100);
            if (randomModifier < 33)
            {
                index = 0;
            }
            else if (randomModifier > 33 && randomModifier < 66)
            {
                index = 1;
            }
            else index = 2;
        }
        else if (uiLevelManager.GetLevelSetting() == 7)
        {
            var randomModifier = UnityEngine.Random.Range(0, 100);
            if (randomModifier < 25)
            {
                index = 0;
            }
            else if (randomModifier > 25 && randomModifier < 40)
            {
                index = 1;
            }
            else index = 2;
        }
        else if (uiLevelManager.GetLevelSetting() == 8)
        {
            var randomModifier = UnityEngine.Random.Range(0, 100);
            if (randomModifier < 10)
            {
                index = 0;
            }
            else if (randomModifier > 10 && randomModifier < 45)
            {
                index = 1;
            }
            else index = 2;
        }
        else if (uiLevelManager.GetLevelSetting() == 9)
        {
            var randomModifier = UnityEngine.Random.Range(0, 100);
            if (randomModifier < 50)
            {
                index = 1;
            }
            else index = 2;
        }
        while (gameManager.GetIsGameActiveBool())
        {
            Instantiate(enemyUnitPrefabs[index], transform.position, Quaternion.identity, transform);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
