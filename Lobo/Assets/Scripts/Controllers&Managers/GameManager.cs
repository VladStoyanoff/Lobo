using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool isGameActive;
    int playerLives = 4;
    [SerializeField] GameObject playerLivesIndicator;

    void Start()
    {
        isGameActive = true;
    }

    public void ReduceLives()
    {
        playerLives--;
        var oneLife = playerLivesIndicator.transform.GetChild(playerLivesIndicator.transform.childCount - 1);
        oneLife.gameObject.SetActive(false);

        if (playerLives != 0) return;
    }

    public bool GetIsGameActiveBool() => isGameActive;
    public int GetPlayerLives() => playerLives;
}
