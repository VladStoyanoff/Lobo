using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = .005f;
    [SerializeField] Transform spawnPoint;
    

    
    void Start()
    {
        var player = FindObjectOfType<PlayerController>();
        spawnPoint = player.transform.GetChild(0).GetChild(0).GetChild(1);
        GetComponent<Rigidbody2D>().velocity = spawnPoint.up * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Building Block"))
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
