using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speedModifier = 1;
    public GameObject player;
    private Rigidbody playerRb;
    private Rigidbody rb;

    void Start() {
        playerRb = player.GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        Vector3 dir = playerRb.position - rb.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speedModifier);
        rb.velocity = transform.rotation * Vector3.forward * speedModifier;
    }

    void OnCollisionEnter() {
        Destroy(this);
    }

}