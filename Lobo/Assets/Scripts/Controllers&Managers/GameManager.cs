using System;
using System.Collections;
using System.Linq;
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

    [SerializeField] GameObject cannonRotationRadar;
    [SerializeField] GameObject baseRadar;
    [SerializeField] GameObject fuelTank;

    public static event EventHandler OnGameStarted;
    public static event EventHandler OnGameEnded;

    InputActions inputActionsScript;

    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Game.Enable();
    }

    void Update()
    {
        StartGame();

        if (FindObjectOfType<Spawner>().GetEnemyBases().Count == 0 && isGameActive)
        {
            StartCoroutine(WinGameScreen());
        }
    }

    void StartGame()
    {
        if (inputActionsScript.Game.StartGame.IsPressed() == false) return;
        if (isGameActive == false) OnGameStarted?.Invoke(this, EventArgs.Empty);
        isGameActive = true;
    }

    public void ReduceLives()
    {
        collided = true;
        playerLives--;
        var oneLife = playerLivesIndicator.transform.GetChild(playerLives);
        oneLife.gameObject.SetActive(false);
        if (playerLives != 0) return;
        OnGameEnded?.Invoke(this, EventArgs.Empty);
        EndGame();
    }

    void EndGame()
    {
        // Reset player lives
        for (int i = 0; i < playerLivesIndicator.transform.childCount; i++)
        {
            playerLivesIndicator.transform.GetChild(i).gameObject.SetActive(true);
            playerLives = 4;
        }

        // Reset radars and fuel tank
        for (int i = 0; i < 7; i++)
        {
            cannonRotationRadar.transform.GetChild(i).gameObject.SetActive(false);
        }
        cannonRotationRadar.transform.GetChild(0).gameObject.SetActive(true);

        for (int i = 0; i < 4; i++)
        {
            baseRadar.transform.GetChild(i).GetComponent<RawImage>().color = Color.black;
        }

        fuelTank.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;

        // Destroy non-persistent objects
        var patrolRoutes = GameObject.FindGameObjectsWithTag("PatrolRoute");
        var enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");
        var mazeNodes = GameObject.FindGameObjectsWithTag("Maze Node");
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        var listOne = patrolRoutes.Concat(enemyBases);
        var listTwo = listOne.Concat(mazeNodes);
        var listWithNonPersistentObjects = listTwo.Concat(enemies);

        foreach (var nonPersistentObject in listWithNonPersistentObjects)
        {
            Destroy(nonPersistentObject.gameObject);
        }
    }

    IEnumerator WinGameScreen()
    {
        endGamePanel.SetActive(true);
        isGameActive = false;
        yield return new WaitForSeconds(3);
        endGamePanel.SetActive(false);
        EndGame();
    }

    public void SetCollidedBool(bool boolean)
    {
        collided = boolean;
    }

    public void SetIsGameActiveBool(bool boolean)
    {
        isGameActive = boolean;
    }

    public bool GetIsGameActiveBool() => isGameActive;
    public int GetPlayerLives() => playerLives;
    public bool GetCollidedBool() => collided;
}
