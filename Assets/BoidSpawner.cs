using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSpawner : MonoBehaviour
{
    public GameObject boidPrefab;
    void Start()
    {
        // start a coroutine to run spawnBoid() every 2 seconds
        StartCoroutine(spawnBoid());
    }

    // Update is called once per frame
    void Update()
    {

    }

    // coroutine to spawn boids
    IEnumerator spawnBoid()
    {
        for(int i=0; i<120; i++)
        {
            // create a new boid
            GameObject boid = Instantiate(boidPrefab, transform.position, Quaternion.identity);
            // set the position of the new boid to a random position on the x-z plane
            //boid.transform.position = new Vector3(Random.Range(-10, 10), 10, Random.Range(-10, 10));
            // set the velocity of the new boid to a random velocity
            //boid.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
            // wait 2 seconds before spawning another boid
            yield return new WaitForSeconds(0.105f);
        }
    }

    // spawn every 2 seconds
    public void SpawnBoid()
    {
        Instantiate(boidPrefab, transform.position, Quaternion.identity);
    }
}
