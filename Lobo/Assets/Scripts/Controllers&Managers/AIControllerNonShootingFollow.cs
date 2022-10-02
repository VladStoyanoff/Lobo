using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControllerNonShootingFollow : MonoBehaviour
{
    PlayerController player;
    NavMeshAgent navMeshAgent;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        navMeshAgent.updateUpAxis = navMeshAgent.updateRotation = false;
        transform.eulerAngles = Vector3.zero;
    }

    void Update()
    {
        FollowPlayerBehaviour();
    }

    void FollowPlayerBehaviour()
    {
        if (player == null) return;
        navMeshAgent.destination = player.transform.position;
    }
}
