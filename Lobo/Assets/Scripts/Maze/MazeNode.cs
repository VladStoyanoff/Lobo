using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode : MonoBehaviour 
{
    [SerializeField] GameObject[] walls;
    
    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].gameObject.SetActive(false);
    }

    public Vector2 GetMazeNodePosition() => transform.position;
}

