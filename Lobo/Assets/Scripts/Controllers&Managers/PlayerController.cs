using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    InputActions inputActionsScript;

    float timeSinceLastShot = Mathf.Infinity;
    const int CANNON_ROTATE_SPEED = 50;
    const float BULLET_SPEED = .5f;
    const int FIRE_RATE = 2;

    Vector2 movementInput;

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject cannon;
    [SerializeField] Transform bulletSpawnPoint;

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
        movementInput = inputActionsScript.Player.Movement.ReadValue<Vector2>();
        transform.position += new Vector3(movementInput.x, movementInput.y, 0) * moveSpeed * Time.deltaTime;
    }

    void TryRotateCannon()
    {
        var rotationVector = Vector3.zero;
        rotationVector.z = inputActionsScript.Player.CannonRotation.ReadValue<float>();
        cannon.transform.localEulerAngles += rotationVector * CANNON_ROTATE_SPEED * Time.deltaTime;
    }

    void TryShootProjectile()
    {
        if (timeSinceLastShot < FIRE_RATE) return;
        if (inputActionsScript.Player.Shoot.IsPressed() == false) return;
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.Euler(cannon.transform.localEulerAngles));
        bullet.tag = "Player Bullet";
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * BULLET_SPEED;
        timeSinceLastShot = 0;
    }

    public Vector2 GetMovementInput() => movementInput;
}
