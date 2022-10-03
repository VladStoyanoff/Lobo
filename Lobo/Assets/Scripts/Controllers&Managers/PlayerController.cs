using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputActions inputActionsScript;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float cannonRotationSpeed = 100;

    float timeSinceLastShot = Mathf.Infinity;
    const int FIRE_RATE = 2;

    Vector2 movementInput;

    GameManager gameManager;
    Spawner spawner;

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject cannon;
    [SerializeField] Transform bulletSpawnPoint;

    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Player.Enable();

        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<Spawner>();
    }

    void Update()
    {
        if (gameManager.GetCollidedBool())
        {
            gameManager.SetCollidedBool();
        }

        timeSinceLastShot += Time.deltaTime;
        UpdateMovement();
        TryRotateCannon();
        TryShootProjectile();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        RestartPlayerPosition();
    }

    public void RestartPlayerPosition()
    {
        if (gameManager.GetCollidedBool()) return;
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
