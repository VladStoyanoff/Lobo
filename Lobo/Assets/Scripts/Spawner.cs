using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    MazeGenerator mazeGenerator;
    GameManager gameManager;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyBasePrefab;

    List<GameObject> enemyBases = new List<GameObject>();

    void Awake()
    {
        mazeGenerator = FindObjectOfType<MazeGenerator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        SpawnPlayer();
        SpawnEnemyBases();
    }

    public void SpawnPlayer()
    {
        var allNodes = mazeGenerator.GetMazeNodesList();
        var spawnPlayerHere = allNodes[Random.Range(0, allNodes.Count)];
        allNodes.Remove(spawnPlayerHere);
        if (FindObjectOfType<PlayerController>() == null)
        {
            playerPrefab = Instantiate(playerPrefab, spawnPlayerHere.transform.position, Quaternion.identity);
        }
        else
        {
            playerPrefab.transform.position = spawnPlayerHere.transform.position;
        }
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
