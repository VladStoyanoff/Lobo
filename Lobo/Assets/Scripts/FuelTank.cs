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
    }

    void Update()
    {
        if (playerController.GetMovementInput() == new Vector2(0, 0)) return;
        fuelTank.fillAmount -= fillAmount;
    }
}
