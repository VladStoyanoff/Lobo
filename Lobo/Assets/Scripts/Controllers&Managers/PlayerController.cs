using System.Collections;
using System;
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
    const float FIRE_RATE = .5f;

    [SerializeField] float moveSpeed;
    Vector2 movementInput;

    [SerializeField] ParticleSystem explosion;

    GameManager gameManager;
    Spawner spawner;
    AudioManager audioManager;

    void Awake()
    {
        inputActionsScript = new InputActions();
        inputActionsScript.Player.Enable();

        gameManager = FindObjectOfType<GameManager>();
        spawner = FindObjectOfType<Spawner>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        UpdateMovement();
        TryRotateCannon();
        TryShootProjectile();

        if (gameManager.GetCollidedBool() == false) return;
        gameManager.SetCollidedBool(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.GetCollidedBool()) return;
        StartCoroutine(RestartPlayerPosition());
    }

    public IEnumerator RestartPlayerPosition()
    {
        var explosionDuration = 1.5f;
        explosion.Play();
        audioManager.PlayDestroyedEnemyClip();
        gameManager.ReduceLives();
        GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(explosionDuration);
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
        audioManager.PlayShootingClip();
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.transform.position, Quaternion.Euler(cannon.transform.localEulerAngles));
        bullet.tag = "Player Bullet";
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
        timeSinceLastShot = 0;
    }

    public Vector2 GetMovementInput() => movementInput;
}
