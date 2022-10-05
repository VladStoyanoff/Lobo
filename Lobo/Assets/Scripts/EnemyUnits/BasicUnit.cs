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

        aiController.RotateTowards(aiController.GetWaypointPosition());
        if (aiController.GetIsNotInRangeOfPlayerBool()) return;

        // Shoot if player is nearby
        if (timeSinceLastShot < FIRE_RATE) return;
        var bullet = Instantiate(aiController.GetBulletPrefab(), bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.tag = "Enemy Bullet";
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * BULLET_SPEED;
        timeSinceLastShot = 0;
    }
}
