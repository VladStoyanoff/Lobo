using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputActions inputActionsScript;

    float timeSinceLastShot = Mathf.Infinity;
    const int CANNON_ROTATE_SPEED = 50;
    const int FIRE_RATE = 2;

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject cannon;
    [SerializeField] GameObject bulletSpawnPoint;


    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Player.Enable();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
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
        cannon.transform.localEulerAngles += rotationVector * CANNON_ROTATE_SPEED * Time.deltaTime;
    }

    // BUGGY - Fix when you've added the cannon
    void TryShootProjectile()
    {
        if (timeSinceLastShot < FIRE_RATE) return;
        if (inputActionsScript.Player.Shoot.IsPressed() == false) return;
        Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.Euler(cannon.transform.localEulerAngles), transform);
        timeSinceLastShot = 0;
    }
}
