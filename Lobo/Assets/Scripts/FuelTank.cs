using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FuelTank : MonoBehaviour
{
    Image fuelTank;
    PlayerController playerController;
    float fillAmount = .0001f;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        fuelTank = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).GetChild(4).GetChild(0).GetChild(0).GetComponent<Image>();
        StartCoroutine(FillTank());
    }

    void Update()
    {
        if (playerController.GetMovementInput() == new Vector2(0, 0)) return;
        fuelTank.fillAmount -= fillAmount;
        if (fuelTank.fillAmount > 0) return;
        playerController.RestartPlayerPosition();
    }

    public void RefillTank()
    {
        fuelTank.fillAmount = 1;
    }

    public void EmptyTank()
    {
        fuelTank.fillAmount = 0;
    }

    IEnumerator FillTank()
    {
        fuelTank.enabled = true;
        fuelTank.fillAmount = 0;
        while (fuelTank.fillAmount < 1)
        {
            fuelTank.fillAmount += .01f;
            yield return new WaitForSeconds(.01f);
        }
    }
}
