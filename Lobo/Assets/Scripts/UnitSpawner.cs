using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    MazeGenerator mazeGenerator;

    void Awake()
    {
        mazeGenerator = FindObjectOfType<MazeGenerator>();
    }

    void Start()
    {
        var allNodes = mazeGenerator.GetMazeNodesList();
        var randomNode = allNodes[Random.Range(0, allNodes.Count)];

        Instantiate(playerPrefab, randomNode.GetMazeNodePosition(), Quaternion.identity);
    }
}
