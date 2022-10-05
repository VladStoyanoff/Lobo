using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    PatrolRouteGenerator patrolRouteGenerator;
    PlayerController player;
    NavMeshAgent navMeshAgent;

    [SerializeField] GameObject bulletPrefab;

    int waypointIndex = 0;

    const float WAYPOINT_WIDTH = .3f;
    const int CHASE_RADIUS = 1;

    Vector3 waypointPosition;
    bool isNotInRangeOfPlayer;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        patrolRouteGenerator = GetComponent<PatrolRouteGenerator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        navMeshAgent.updateUpAxis = navMeshAgent.updateRotation = false;
        transform.eulerAngles = Vector3.zero;
    }

    void Update()
    {
        PatrolBehaviour();
        AttackBehaviour();
    }

    void PatrolBehaviour()
    {
        AssignPatrolAndCurrentWaypoint(out waypointPosition, out var waypointList);
        StartApproachingNextWaypoint(waypointPosition);
        RestartPatrolRoute(waypointList);
    }

    void AttackBehaviour()
    {
        if (player == null) return;
        CheckIfPlayerIsInRange(out isNotInRangeOfPlayer);
        if (isNotInRangeOfPlayer) return;
    }

    void AssignPatrolAndCurrentWaypoint(out Vector3 waypointPosition, out List<Transform> waypointList)
    {
        var list = patrolRouteGenerator.GetWaypointsList();
        var waypoint = list[waypointIndex].position;
        waypointList = list;
        waypointPosition = waypoint;
        navMeshAgent.destination = waypointPosition;
    }

    void StartApproachingNextWaypoint(Vector3 waypointPosition)
    {
        var distanceToWaypoint = Vector3.Distance(transform.position, waypointPosition);
        if (distanceToWaypoint > WAYPOINT_WIDTH) return;
        waypointIndex++;
    }

    void RestartPatrolRoute(List<Transform> waypointList)
    {
        if (waypointIndex != waypointList.Count) return;
        waypointIndex = 0;
    }

    void CheckIfPlayerIsInRange(out bool isNotInRangeOfPlayer)
    {
        var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        var check = distanceToPlayer > CHASE_RADIUS;
        isNotInRangeOfPlayer = check;
    }

    public Vector3 GetWaypointPosition() => waypointPosition;
    public bool GetIsNotInRangeOfPlayerBool() => isNotInRangeOfPlayer;
    public GameObject GetBulletPrefab() => bulletPrefab;
    public PlayerController GetPlayerController() => player;
}
