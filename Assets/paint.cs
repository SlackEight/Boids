using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paint : MonoBehaviour
{

    List<GameObject> neighbours = new List<GameObject>();
    Transform fish;
    public float alignment;
    Rigidbody rb;
    float speed;

    public Material paint_mat;
    public Material normal_mat;
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
            paint_mat.color = Color.green;
            other.GetComponent<MeshRenderer>().material = paint_mat;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Boid")
        {
            Debug.Log("FISH OUT");
            other.GetComponent<MeshRenderer>().material = normal_mat;
            neighbours.Remove(other.gameObject);
        }
    }

    void align() // alignment
    {
        if(neighbours.Count > 0){
            Vector3 average_velocity = Vector3.zero;
            foreach (GameObject n in neighbours)
            {
                average_velocity += n.GetComponent<Rigidbody>().velocity;
            }
            average_velocity /= neighbours.Count;
            
            // move towards average velocity
            Vector3 direction = average_velocity - rb.velocity;
            rb.velocity += direction.normalized*alignment;
        }
    }
}
