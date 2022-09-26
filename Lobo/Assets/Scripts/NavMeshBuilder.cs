using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBuilder : MonoBehaviour
{
    void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }
}