using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigation : MonoBehaviour
{
    [SerializeField] GameObject radar;
    [SerializeField] List<SpriteRenderer> sprites;
    [SerializeField] float[] arrayWithConstants;
    Spawner spawner;

    void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        
    }

    void Update()
    {
        UpdateEnemyBaseLocations();
        UpdatePlayerDirectionRadar();
        UpdatePlayerLocation();
    }

    void UpdateEnemyBaseLocations()
    {
        for (int i = 0; i < 8; i++)
        {
            var checkConstant = arrayWithConstants[i];
            var checkConstant2 = arrayWithConstants[i + 1];
            CheckRadarDirection(checkConstant, checkConstant2);
        }
    }

    void CheckRadarDirection(int a, int b)
    {
        var enemyBases = spawner.GetEnemyBases();
        var arrayWithDistances[];
        foreach (GameObject enemyBase in enemyBases)
        {
            var distanceToBase = Vector2.Distance(gameObject, enemyBase);
            arrayWithDistances[distanceToBase];
            
        }
        if (Vector2.Distance(gameObject, closestEnemyBase) <?> a && <?> b) return;
        updatesprite;
    }

    void UpdatePlayerDirectionRadar()
    {

    }

    void UpdatePlayerLocation()
    {

    }
}
