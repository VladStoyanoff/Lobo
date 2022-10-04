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
    [SerializeField] GameObject endGamePanel;

    public static event EventHandler OnGameStarted;
    public static event EventHandler OnGameEnded;

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
        EndGame();
    }

    public void ReduceLives()
    {
        collided = true;
        playerLives--;
        var oneLife = playerLivesIndicator.transform.GetChild(playerLives);
        oneLife.gameObject.SetActive(false);
        if (playerLives != 0) return;
        OnGameEnded?.Invoke(this, EventArgs.Empty);
        EndBehaviour();
    }

    void EndBehaviour()
    {
        var patrolRoutes = GameObject.FindGameObjectsWithTag("PatrolRoute");
        foreach (var patrolRoute in patrolRoutes)
        {
            Destroy(patrolRoute.gameObject);
        }
        var enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");
        foreach (var enemyBase in enemyBases)
        {
            Destroy(enemyBase.gameObject);
        }
        var mazeNodes = GameObject.FindGameObjectsWithTag("Maze Node");
        foreach (var mazeNode in mazeNodes)
        {
            Destroy(mazeNode.gameObject);
        }
        for (int i = 0; i < playerLivesIndicator.transform.childCount; i++)
        {
            playerLivesIndicator.transform.GetChild(i).gameObject.SetActive(true);
            playerLives = 4;
        }

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        for (int i = 0; i < 7; i++)
        {
            GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetChild(6).GetChild(0).GetChild(0).GetChild(i + 1).GetComponent<RawImage>().gameObject.SetActive(false);
        }
        GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetChild(6).GetChild(0).GetChild(0).GetChild(0).GetComponent<RawImage>().gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetChild(5).GetChild(i).GetComponent<RawImage>().color = Color.black;
        }

        GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount = 0;
    }

    void EndGame()
    {
        if (FindObjectOfType<Spawner>().GetEnemyBases().Count == 0 && isGameActive)
        {
            StartCoroutine(EndGameScreen());
        }
    }

    IEnumerator EndGameScreen()
    {
        endGamePanel.SetActive(true);
        isGameActive = false;
        yield return new WaitForSeconds(3);
        endGamePanel.SetActive(false);
        EndBehaviour();
    }

    public void SetCollidedBool(bool boolean)
    {
        collided = boolean;
    }

    public void SetIsGameActiveBool(bool boolean)
    {
        isGameActive = boolean;
    }

    void StartGame()
    {
        if (inputActionsScript.Game.StartGame.IsPressed() == false) return;
        if (isGameActive == false) OnGameStarted?.Invoke(this, EventArgs.Empty);
        isGameActive = true;
    }

    public bool GetIsGameActiveBool() => isGameActive;
    public int GetPlayerLives() => playerLives;
    public bool GetCollidedBool() => collided;
}
