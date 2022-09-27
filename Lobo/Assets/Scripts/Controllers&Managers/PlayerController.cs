using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputActions inputActionsScript;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bulletPrefab;

    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Player.Enable();
    }

    void Update()
    {
        UpdateMovement();
        TryShootProjectile();
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(gameObject);
    //}

    void UpdateMovement()
    {
        var input = inputActionsScript.Player.Movement.ReadValue<Vector2>();
        transform.position += new Vector3(input.x, input.y, 0) * Time.deltaTime * moveSpeed;
    }

    // BUGGY - Fix when you've added the cannon
    void TryShootProjectile()
    {
        if (inputActionsScript.Player.Shoot.IsPressed() == false) return;
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
