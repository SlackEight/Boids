using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    Rigidbody rb;
    float speed;

    public float aviodance;
    List<GameObject> neighbours = new List<GameObject>();
    List<GameObject> collidable_objects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
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

        avoid_collision();
        rb.velocity = rb.velocity.normalized*speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boid")
        {
            other.GetComponentInChildren<Cohere>().coherence=0;
            other.GetComponentInChildren<Avoid>().aviodance=0.6f;
            other.GetComponentInChildren<Align>().alignment=0;
            other.GetComponent<Boid>().speed *=2;
            Debug.Log("Boid entered");
        }
        else if(other.gameObject.tag == "Collidable")
        {
            collidable_objects.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boid")
        {
            other.GetComponentInChildren<Cohere>().coherence=0.03f;
            other.GetComponentInChildren<Avoid>().aviodance=0.06f;
            other.GetComponentInChildren<Align>().alignment=0.06f;
            other.GetComponent<Boid>().speed /=2;
        }
        else if(other.gameObject.tag == "Collidable")
        {
            collidable_objects.Remove(other.gameObject);
        }
    }

    void avoid_collision() // seperation
    {
        foreach (GameObject neighbour in neighbours)
        {
            Vector3 direction = transform.position - neighbour.transform.position;
            rb.velocity -= direction.normalized * aviodance;///(direction.magnitude*50);
        }

        foreach (GameObject collidable_object in collidable_objects)
        {
            if(collidable_objects.Count>0){
                Vector3 direction = transform.position - collidable_object.GetComponent<Collider>().ClosestPoint(transform.position);
                if ((direction.normalized * 5f)/(direction.magnitude*10) != null)
                rb.velocity += (direction.normalized * 5f)/(direction.magnitude*10);
                else{
                    Debug.Log(collidable_object.name);
                }
            }
        }
    }
}
