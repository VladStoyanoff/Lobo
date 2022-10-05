using UnityEngine;
using UnityEngine.AI;

public class ChasePlayerUnit : MonoBehaviour
{
    AIController aiController;
    NavMeshAgent navMesh;
    [SerializeField] GameObject bulletSpawnPoint;
    float timeSinceLastShot = Mathf.Infinity;

    const int FIRE_RATE = 2;
    const float BULLET_SPEED = 2f;

    void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        aiController = GetComponent<AIController>();
    }

    void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (aiController.GetIsNotInRangeOfPlayerBool()) return;
        navMesh.destination = aiController.GetPlayerController().transform.position;
        aiController.RotateTowards(navMesh.destination);

        if (timeSinceLastShot < FIRE_RATE) return;
        var bullet = Instantiate(aiController.GetBulletPrefab(), bulletSpawnPoint.transform.position, Quaternion.identity);
        bullet.tag = "Enemy Bullet";
        bullet.GetComponent<Rigidbody2D>().velocity = (aiController.GetPlayerController().transform.position - transform.position).normalized * BULLET_SPEED;
        timeSinceLastShot = 0;
    }
}
