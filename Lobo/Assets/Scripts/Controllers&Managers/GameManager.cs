using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isGameActive;

    void Start()
    {
        isGameActive = true;
    }

    public bool GetIsGameActiveBool() => isGameActive;
}
