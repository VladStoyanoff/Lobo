using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        var speed = 0f;
        var player = FindObjectOfType<PlayerController>();
        GetComponent<Rigidbody2D>().velocity = (player.transform.position - transform.position).normalized * speed;
    }
}
