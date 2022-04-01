using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{
    List<GameObject> neighbours = new List<GameObject>();
    Transform fish;
    public float alignment;
    Rigidbody rb;
    float speed;
    void Start()
    {
        // generate random speed
        speed = 2f;//Random.Range(1f, 10f);
        rb = GetComponentInParent<Rigidbody>();
        fish = GetComponentInParent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        align();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boid")
        {
            neighbours.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boid")
        {
            neighbours.Remove(other.gameObject);
        }
    }

    void align() // alignment
    {
        if(neighbours.Count>0){
            Vector3 average_velocity = Vector3.zero;
            for (int i = 0; i < neighbours.Count; i++)
            {
                average_velocity += neighbours[i].GetComponent<Rigidbody>().velocity;
            }
            average_velocity /= neighbours.Count;
            
            // move towards average velocity
            Vector3 direction = average_velocity - rb.velocity;
            rb.velocity += direction.normalized*alignment;
        }
    }
}
