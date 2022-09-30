using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineCamera;
    PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null) return;
        cinemachineCamera.LookAt = cinemachineCamera.Follow = playerController.transform;
    }

    // FIX: Boundaries on the far outmost parts of the level: Camera shouldnt see the void
}
