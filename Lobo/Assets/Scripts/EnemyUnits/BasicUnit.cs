using UnityEngine;

public class BasicUnit : MonoBehaviour
{
    [SerializeField] Transform bulletSpawnPoint;
    AIController aiController;

    float timeSinceLastShot = Mathf.Infinity;
    const int FIRE_RATE = 2;
    const float BULLET_SPEED = 2f;

    void Start() 
    {
        aiController = GetComponent<AIController>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        // Rotate towards current waypoint position
        var angle = Mathf.Atan2(aiController.GetWaypointPosition().y - transform.position.y, aiController.GetWaypointPosition().x - transform.position.x) * Mathf.Rad2Deg;
        var targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100 * Time.deltaTime);
        if (aiController.GetIsNotInRangeOfPlayerBool()) return;

        // Shoot if player is nearby
        if (timeSinceLastShot < FIRE_RATE) return;
        var bullet = Instantiate(aiController.GetBulletPrefab(), transform.position, Quaternion.Euler(bulletSpawnPoint.transform.localEulerAngles));
        bullet.tag = "Enemy Bullet";
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * BULLET_SPEED;
        timeSinceLastShot = 0;
    }
}
