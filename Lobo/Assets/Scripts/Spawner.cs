using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    MazeGenerator mazeGenerator;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyBasePrefab;

    List<GameObject> enemyBases = new List<GameObject>();

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
            allNodes.Remove(randomNode);
            var enemyBase = Instantiate(enemyBasePrefab, randomNode.GetMazeNodePosition(), Quaternion.identity, transform);
            enemyBases.Add(enemyBase);
        }
    }

    public List<GameObject> GetEnemyBases() => enemyBases;
}
