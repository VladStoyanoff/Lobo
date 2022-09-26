using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    PlayerController player;
    MazeGenerator mazeGenerator;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        mazeGenerator = FindObjectOfType<MazeGenerator>();
    }

    void Start()
    {
        var agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    //void Start()
    //{
    //    // assign a random patrol route
    //    // roam
    //    // attack
    //}



    //void AttackBehaviour()
    //{
    //    var stopChasingVariable = 5;
    //    var distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
    //    var isNotInRangeOfPlayer = distanceToPlayer > stopChasingVariable;
    //    if (isNotInRangeOfPlayer) return;
    //    transform.LookAt(player.transform);
    //    ShootProjectile();
    //}

    //void ShootProjectile()
    //{
    //    throw new NotImplementedException();
    //}
}
