using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputActions inputActionsScript;
    [SerializeField] float moveSpeed;

    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Movement.Enable();
    }

    void Update()
    {
        var input = inputActionsScript.Movement.Movement.ReadValue<Vector2>();
        transform.position += new Vector3(input.x, input.y, 0) * Time.deltaTime * moveSpeed;
    }
}
