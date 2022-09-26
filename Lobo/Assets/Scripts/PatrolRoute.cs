using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolRoute : MonoBehaviour
{
    [SerializeField] Transform waypointPrefab;

    MazeGenerator mazeGenerator;
    NavMeshAgent navMeshAgent;
    List<Transform> waypoints = new List<Transform>();
    int waypointIndex = 0;

    void Awake()
    {
        mazeGenerator = FindObjectOfType<MazeGenerator>();
    }

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

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
        var waypointWidth = .3f;
        var waypointPosition = waypoints[waypointIndex].position;
        navMeshAgent.destination = waypointPosition;
        var distanceToWaypoint = Vector3.Distance(transform.position, waypointPosition);
        if (distanceToWaypoint > waypointWidth) return;
        waypointIndex++;
        if (waypointIndex != waypoints.Count) return;
        waypointIndex = 0;
    }
}