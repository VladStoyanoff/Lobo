using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineCamera;

    void Start()
    {
        var player = FindObjectOfType<PlayerController>();
        cinemachineCamera.LookAt = cinemachineCamera.Follow = player.transform;
    }

    // FIX: Boundaries on the far outmost parts of the level: Camera shouldnt see the void
}
