using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : MonoBehaviour
{   
    public float aviodance;
    List<GameObject> neighbours = new List<GameObject>();
    List<GameObject> collidable_objects = new List<GameObject>();
    Transform fish;
    Rigidbody rb;
    float speed;
    void Start()
    {
        // generate random speed
        rb = GetComponentInParent<Rigidbody>();
        fish = GetComponentInParent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        // face the direction of the velocity
        fish.rotation = Quaternion.LookRotation(rb.velocity);
        
        //rotate an extra 90 on the y axis
        fish.Rotate(0, 90, 0);

        avoid_collision();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Boid")
        {
            neighbours.Add(other.gameObject);
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
            neighbours.Remove(other.gameObject);
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
            Vector3 direction = fish.position - neighbour.transform.position;
            rb.velocity += direction.normalized * aviodance;///(direction.magnitude*50);
        }

        foreach (GameObject collidable_object in collidable_objects)
        {
            if(collidable_objects.Count>0){
                Vector3 direction = fish.position - collidable_object.GetComponent<Collider>().ClosestPoint(fish.position);
                if ((direction.normalized * 10f)/(direction.magnitude*10) != null)
                rb.velocity += (direction.normalized * 3f)/(direction.magnitude*10);
                else{
                    Debug.Log(collidable_object.name);
                }
            }
        }
    }
}
