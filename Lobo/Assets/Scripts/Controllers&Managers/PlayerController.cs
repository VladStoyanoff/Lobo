using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float timeSinceLastShot = Mathf.Infinity;
    InputActions inputActionsScript;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject cannon;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float cannonRotationSpeed = 100;
    const int FIRE_RATE = 2;

    [SerializeField] float moveSpeed;
    Vector2 movementInput;

    GameManager gameManager;
    Spawner spawner;

    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Player.Enable();

        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        UpdateMovement();
        TryRotateCannon();
        TryShootProjectile();

        if (gameManager.GetCollidedBool() == false) return;
        gameManager.SetCollidedBool();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.GetCollidedBool()) return;
        RestartPlayerPosition();
    }

    public void RestartPlayerPosition()
    {
        gameManager.ReduceLives();
        spawner.SpawnPlayer();
    }

    void UpdateMovement()
    {
        movementInput = inputActionsScript.Player.Movement.ReadValue<Vector2>();
        transform.position += new Vector3(movementInput.x, movementInput.y, 0) * moveSpeed * Time.deltaTime;
    }

    void TryRotateCannon()
    {
        var rotationVector = Vector3.zero;
        rotationVector.z = inputActionsScript.Player.CannonRotation.ReadValue<float>();
        cannon.transform.localEulerAngles += rotationVector * cannonRotationSpeed * Time.deltaTime;
    }

    void TryShootProjectile()
    {
        if (timeSinceLastShot < FIRE_RATE) return;
        if (inputActionsScript.Player.Shoot.IsPressed() == false) return;
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.Euler(cannon.transform.localEulerAngles));
        bullet.tag = "Player Bullet";
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
        timeSinceLastShot = 0;
    }

    public Vector2 GetMovementInput() => movementInput;
}
