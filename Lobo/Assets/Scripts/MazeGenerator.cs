using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector2Int mazeSize;

    void Start()
    {
        GenerateMaze(mazeSize);
    }

    void GenerateMaze(Vector2Int mazeSize)
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
            }
        }

        var currentPath = new List<MazeNode>();
        var completedNodes = new List<MazeNode>();

        // Choose Starting Node
        currentPath.Add(nodes[Random.Range(0, nodes.Count)]);

        while (completedNodes.Count < nodes.Count)
        {
            var possibleNextNodes = new List<int>();
            var possibleDirections = new List<int>();

            var currentNodeIndex = nodes.IndexOf(currentPath[currentPath.Count - 1]);
            var currentNodeX = currentNodeIndex / mazeSize.y;
            var currentNodeY = currentNodeIndex % mazeSize.y;

            void CheckNeighbourNode(int a, int b, bool c, int d, int e, int f)
            {
                if (a >= b == c) return;
                var index = currentNodeIndex + d * e;
                if (completedNodes.Contains(nodes[index])) return;
                if (currentPath.Contains(nodes[index])) return;
                possibleDirections.Add(f);
                possibleNextNodes.Add(index);
            }
            // Left
            CheckNeighbourNode(currentNodeX, mazeSize.x - 1,  true,  1, mazeSize.y, 1);
            // Right
            CheckNeighbourNode(currentNodeX,              1, false, -1, mazeSize.y, 2);
            // Top
            CheckNeighbourNode(currentNodeY, mazeSize.y - 1,  true,  1,          1, 3);
            // Down
            CheckNeighbourNode(currentNodeY,              1, false, -1,          1, 4);


            // Choose next node
            if (possibleDirections.Count > 0)
            {
                var chosenDirection = Random.Range(0, possibleDirections.Count);
                var chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection]) 
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPath[currentPath.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPath[currentPath.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPath[currentPath.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPath[currentPath.Count - 1].RemoveWall(3);
                        break;
                }

                currentPath.Add(chosenNode);
            }

            // Backtrack
            else
            {
                completedNodes.Add(currentPath[currentPath.Count - 1]);
                currentPath.RemoveAt(currentPath.Count - 1);
            }
        }
    }
}
