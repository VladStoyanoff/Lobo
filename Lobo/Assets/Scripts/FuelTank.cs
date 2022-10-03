using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelTank : MonoBehaviour
{
    [SerializeField] Image fuelTank;

    PlayerController playerController;
    float fillAmount = .0001f;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        fuelTank = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>();
        GameManager.OnGameStarted += GameManager_OnGameStarted;
    }

    void GameManager_OnGameStarted(object sender, EventArgs e)
    {
        RefillTankSlow();
    }

    void Update()
    {
        if (playerController.GetMovementInput() == new Vector2(0, 0)) return;
        fuelTank.fillAmount -= fillAmount;
        if (fuelTank.fillAmount <= 0)
        {
            playerController.RestartPlayerPosition();
        }
    }

    public void RefillTank()
    {
        fuelTank.fillAmount = 1;
    }

    public void RefillTankSlow()
    {
        StartCoroutine(RefillTankSlowCoroutine());
    }

    IEnumerator RefillTankSlowCoroutine()
    {
        while(GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount < 1)
        {
            GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>().fillAmount += .0001f;
            yield return new WaitForSeconds(.1f);
        }
    }
}
