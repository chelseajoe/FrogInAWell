using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obs1, obs2, obs3, obs4, obs5;
    private float obsSpawnInterval;
    float speedUp;


    void Start()
    {
        StartCoroutine("SpawnObstacles");
    }

    void SpawnObstacle()
    {
        int direction = Random.Range(1, 3);
        if (direction == 1) {
            transform.position = new Vector3(-.1f, 3f, 0f);

            int random = Random.Range(1, 5);
            if (random == 1)
            {
                Instantiate(obs1, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }

            else if (random == 2)
            {
                Instantiate(obs2, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }

            else if (random == 3)
            {
                Instantiate(obs5, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }

            else if (random == 4)
            {
                Instantiate(obs4, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }

        } else {
            transform.position = new Vector3(-1.6f, 3f, 0f);

            int random = Random.Range(1, 5);
            if (random == 1)
            {
                Instantiate(obs1, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }

            else if (random == 2)
            {
                Instantiate(obs2, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }

            else if (random == 3)
            {
                Instantiate(obs3, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }

            else if (random == 4)
            {
                Instantiate(obs4, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            }
        }
    }
    
    IEnumerator SpawnObstacles()
    {
        while(true)
        {
            SpawnObstacle();
            obsSpawnInterval = Random.Range(0.5f, 2f);
            yield return new WaitForSeconds(obsSpawnInterval);
        }
    }
}
