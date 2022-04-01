using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    int num_neighbours = 0;
    int num_close_collidables = 0;
    Rigidbody rb;
    public float speed;
    void Start()
    {
                // generate random speed
        speed = 5f;//Random.Range(1f, 10f);
        rb = GetComponent<Rigidbody>();
        rb.velocity = Random.insideUnitSphere * speed;

    }

    // Update is called once per frame
    void Update()
    {
        // face the direction of the velocity
        transform.rotation = Quaternion.LookRotation(rb.velocity);
        
        //rotate an extra 90 on the y axis
        transform.Rotate(0, 90, 0);

        rb.velocity = rb.velocity.normalized*speed;

        //avoid_collision();
    }

}
