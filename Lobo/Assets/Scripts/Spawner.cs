using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    MazeGenerator mazeGenerator;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject enemyBasePrefab;

    GameObject player;

    List<GameObject> enemyBases = new List<GameObject>();

    void Start()
    {
        GameManager.OnGameStarted += GameManager_OnGameStarted;
    }

    void GameManager_OnGameStarted(object sender, EventArgs e)
    {
        mazeGenerator = FindObjectOfType<MazeGenerator>();
        SpawnPlayer();
        SpawnEnemyBases();
    }

    public void SpawnPlayer()
    {
        var allNodes = mazeGenerator.GetMazeNodesList();
        var spawnPlayerHere = allNodes[UnityEngine.Random.Range(0, allNodes.Count)];
        allNodes.Remove(spawnPlayerHere);
        if (FindObjectOfType<PlayerController>() == null)
        {
            player = Instantiate(playerPrefab, spawnPlayerHere.transform.position, Quaternion.identity);
        }
        else
        {
            player.transform.position = spawnPlayerHere.transform.position;
        }
    }

    void SpawnEnemyBases()
    {
        enemyBases.Clear();
        for (int i = 0; i < 6; i++)
        {
            var allNodes = mazeGenerator.GetMazeNodesList();
            var randomNode = allNodes[UnityEngine.Random.Range(0, allNodes.Count)];
            allNodes.Remove(randomNode);
            var enemyBase = Instantiate(enemyBasePrefab, randomNode.GetMazeNodePosition(), Quaternion.identity, transform);
            enemyBases.Add(enemyBase);
        }
    }

    public List<GameObject> GetEnemyBases() => enemyBases;
}
