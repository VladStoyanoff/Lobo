using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector2Int mazeSize;

    void Start()
    {
        StartCoroutine(GenerateMaze(mazeSize));
    }

    IEnumerator GenerateMaze(Vector2Int mazeSize)
    {
        var nodes = new List<MazeNode>();
        // Create Nodes
        for (int x = 0; x < mazeSize.x; x++)
        {
            for (int y = 0; y < mazeSize.y; y++)
            {
                // centers the maze around (0,0) instead of the far left corner
                var nodePos = new Vector2(x - (mazeSize.x / 2f), y - (mazeSize.y / 2f));
                var newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                nodes.Add(newNode);
                yield return null;
            }
        }

        var currentPath = new List<MazeNode>();
        var completedNodes = new List<MazeNode>();

        // Choose Starting Node
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);
        currentPath[0].SetState(NodeState.Current);

        while(completedNodes.Count < nodes.Count)
        {
            // Check nodex next to the current Node
            var possibleNextNode = new List<int>();
        }
    }
}
