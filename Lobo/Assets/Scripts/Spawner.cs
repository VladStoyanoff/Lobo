using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyBasePrefab;
    MazeGenerator mazeGenerator;

    void Awake()
    {
        mazeGenerator = FindObjectOfType<MazeGenerator>();
    }

    void Start()
    {
        SpawnPlayer();
        SpawnEnemyBases();
    }

    void SpawnPlayer()
    {
        var allNodes = mazeGenerator.GetMazeNodesList();
        var spawnPlayerHere = allNodes[Random.Range(0, allNodes.Count)];

        Instantiate(playerPrefab, spawnPlayerHere.GetMazeNodePosition(), Quaternion.identity);
        allNodes.Remove(spawnPlayerHere);
    }

    void SpawnEnemyBases()
    {
        for (int i = 0; i < 6; i++)
        {
            var allNodes = mazeGenerator.GetMazeNodesList();
            var randomNode = allNodes[Random.Range(0, allNodes.Count)];
            Instantiate(enemyBasePrefab, randomNode.GetMazeNodePosition(), Quaternion.identity, transform);
            allNodes.Remove(randomNode);
        }
    }
}
