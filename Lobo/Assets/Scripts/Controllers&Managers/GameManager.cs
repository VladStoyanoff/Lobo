using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    bool collided;
    bool isGameActive;
    int playerLives = 4;
    [SerializeField] GameObject playerLivesIndicator;
    [SerializeField] GameObject menuPanel;
    [SerializeField] Image coverImage;

    public static event EventHandler OnGameStarted;

    InputActions inputActionsScript;
    UIManager uiManager;

    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Game.Enable();

        uiManager = FindObjectOfType<UIManager>();
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

            var patrolRoutes = GameObject.FindGameObjectsWithTag("PatrolRoute");
            foreach(var patrolRoute in patrolRoutes)
            {
                Destroy(patrolRoute.gameObject);
            }
            var enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");
            foreach (var enemyBase in enemyBases)
            {
                Destroy(enemyBase.gameObject);
            }
            Destroy(FindObjectOfType<PlayerController>().gameObject);
            var mazeNodes = GameObject.FindGameObjectsWithTag("Maze Node");
            foreach (var mazeNode in mazeNodes)
            {
                Destroy(mazeNode.gameObject);
            }
            isGameActive = false;
            for (int i = 0; i < playerLivesIndicator.transform.childCount; i++)
            {
                playerLivesIndicator.transform.GetChild(i).gameObject.SetActive(true);
                playerLives = 4;
            }
            coverImage.gameObject.SetActive(true);
            FindObjectOfType<ScoreManager>().TrySaveBestScore();
            FindObjectOfType<ScoreManager>().LoadBestScore();
        }
    }

    public void SetCollidedBool()
    {
        collided = false;
    }

    void StartGame()
    {
        if (inputActionsScript.Game.StartGame.IsPressed() == false) return;
        if (uiManager.GetLevelSettingBool() == false ||
            uiManager.GetDensitySettingBool() == false)
        {
            Debug.LogError("In order to start the game, the level and density settings must be set");
        }

        menuPanel.SetActive(false);
        if (isGameActive == false) OnGameStarted?.Invoke(this, EventArgs.Empty);
        coverImage.gameObject.SetActive(false);
        isGameActive = true;
    }

    public bool GetIsGameActiveBool() => isGameActive;
    public int GetPlayerLives() => playerLives;
    public bool GetCollidedBool() => collided;
}
