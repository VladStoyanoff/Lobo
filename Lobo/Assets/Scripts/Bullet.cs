using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = .5f;
    void Start()
    {
        var player = FindObjectOfType<PlayerController>();
        GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * bulletSpeed;
    }
}
