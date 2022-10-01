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
    }

    void Update()
    {
        if (playerController.GetMovementInput() == new Vector2(0, 0)) return;
        fuelTank.fillAmount -= fillAmount;
    }

    public void RefillTank()
    {
        fuelTank.fillAmount = 1;
    }
}
