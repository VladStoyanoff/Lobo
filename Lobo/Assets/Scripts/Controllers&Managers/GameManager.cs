using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    bool collided;
    bool isGameActive;
    int playerLives = 4;
    [SerializeField] GameObject playerLivesIndicator;
    [SerializeField] GameObject menuPanel;

    public static event EventHandler OnGameStarted;

    InputActions inputActionsScript;

    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Game.Enable();
    }

    void Update()
    {
        StartGame();
    }

    public void ReduceLives()
    {
        collided = true;
        playerLives--;
        var oneLife = playerLivesIndicator.transform.GetChild(playerLives);
        oneLife.gameObject.SetActive(false);
        if (playerLives == 0)
        {
            menuPanel.SetActive(true);
        }
    }

    public void SetCollidedBool()
    {
        collided = false;
    }

    void StartGame()
    {
        if (inputActionsScript.Game.StartGame.IsPressed() == false) return;
        menuPanel.SetActive(false);
        OnGameStarted?.Invoke(this, EventArgs.Empty);
        isGameActive = true;
    }

    public bool GetIsGameActiveBool() => isGameActive;
    public int GetPlayerLives() => playerLives;
    public bool GetCollidedBool() => collided;
}
