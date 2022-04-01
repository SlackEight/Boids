using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohere : MonoBehaviour
{
    List<GameObject> neighbours = new List<GameObject>();
    Transform fish;
    public float coherence;
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
        cohere();
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

    void cohere() // coherence
    {
        if(neighbours.Count>0){
            Vector3 average_position = fish.transform.position;
            for (int i = 0; i < neighbours.Count; i++)
            {
                average_position += neighbours[i].transform.position;
            }
            average_position /= neighbours.Count;
            Vector3 direction = average_position - transform.position;
            rb.velocity += direction.normalized * coherence;
            }
    }
}
