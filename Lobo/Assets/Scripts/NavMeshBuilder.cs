using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour
{
    void Start()
    {
        GameManager.OnGameStarted += GameManager_OnGameStarted;
    }
    void GameManager_OnGameStarted(object sender, EventArgs e)
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}