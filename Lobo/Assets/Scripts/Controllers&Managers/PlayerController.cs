using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputActions inputActionsScript;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject bulletSpawnPoint;

    const int CANNON_ROTATE_SPEED = 50;

    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Player.Enable();
    }

    void Update()
    {
        UpdateMovement();
        TryRotateCannon();
        TryShootProjectile();
    }

    //void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Destroy(gameObject);
    //}

    void UpdateMovement()
    {
        var input = inputActionsScript.Player.Movement.ReadValue<Vector2>();
        transform.position += new Vector3(input.x, input.y, 0) * moveSpeed * Time.deltaTime;
    }

    void TryRotateCannon()
    {
        var rotationVector = Vector3.zero;
        rotationVector.z = inputActionsScript.Player.CannonRotation.ReadValue<float>();
        cannon.transform.eulerAngles += rotationVector * CANNON_ROTATE_SPEED * Time.deltaTime;
    }

    // BUGGY - Fix when you've added the cannon
    void TryShootProjectile()
    {
        if (inputActionsScript.Player.Shoot.IsPressed() == false) return;
        Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.identity);
    }
}
