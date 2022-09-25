using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolRoute : MonoBehaviour
{
    [SerializeField] Transform waypointPrefab;

    MazeGenerator mazeGenerator;
    AIController aiController;
    List<Transform> waypoints = new List<Transform>();
    int waypointIndex = 0;

    void Awake()
    {
        aiController = GetComponent<AIController>();
        mazeGenerator = FindObjectOfType<MazeGenerator>();
    }

    void Start()
    {
        var allNodes = mazeGenerator.GetMazeNodesList();
        var numberOfWaypoints = 10;
        for (int i = 0; i < numberOfWaypoints; i++)
        {
            var randomNode = allNodes[Random.Range(0, allNodes.Count)];
            var waypoint = Instantiate(waypointPrefab, randomNode.GetMazeNodePosition(), Quaternion.identity);
            waypoints.Add(waypoint);
        }
    }

    void Update()
    {
        var waypointPosition = waypoints[waypointIndex].position;
        var deltaSpeed = aiController.GetSpeed() * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, waypointPosition, deltaSpeed);
        if (transform.position != waypointPosition) return;
        waypointIndex++;
        if (waypointIndex != waypoints.Count) return;
        waypointIndex = 0;
    }
}