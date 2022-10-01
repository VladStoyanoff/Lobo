using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool collided;
    bool isGameActive;
    int playerLives = 4;
    [SerializeField] GameObject playerLivesIndicator;

    void Start()
    {
        isGameActive = true;
    }

    public void ReduceLives()
    {
        collided = true;
        playerLives--;
        var oneLife = playerLivesIndicator.transform.GetChild(playerLives);
        oneLife.gameObject.SetActive(false);
    }

    public void SetCollidedBool()
    {
        collided = false;
    }

    public bool GetIsGameActiveBool() => isGameActive;
    public int GetPlayerLives() => playerLives;
    public bool GetCollidedBool() => collided;
}
