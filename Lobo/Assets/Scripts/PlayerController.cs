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
        var moveVector = new Vector2(input.x, input.y) * moveSpeed;
        transform.position += new Vector3(moveVector.x, moveVector.y, 0) * Time.deltaTime;
    }
}
