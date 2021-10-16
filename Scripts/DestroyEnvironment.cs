using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class DestroyEnvironment : MonoBehaviour
{
    public GameObject[] obstacles;
    [SerializeField, Range(0, 50)]
    private int minimumObstacle = 0;
    [SerializeField, Range(0,50)]
    private int maximumObstacle = 3;

    public GameObject[] specialObstacles;
    [SerializeField, Range(0, 50)]
    private int minimumSpecialObstacle = 0;
    [SerializeField, Range(0, 50)]
    private int maximumSpecialObstacle = 1;

    public GameObject[] environmentDecoration;
    [SerializeField, Range(0, 50)]
    private int maximumEnvironmentDecoration = 1;

    public GameManager gameManager;
    Rigidbody2D player;

    void Start()
    {
        player = GameObject.Find("Dino").GetComponent<Rigidbody2D>();
        int numberOfObstacles = Random.Range(minimumObstacle, maximumObstacle + 1);
        float[] positions = new float[numberOfObstacles];
        for (int i = 0; i < numberOfObstacles; i++)
        {
            bool spawnable = false;
            int randomObstacle = Random.Range(0, obstacles.Length);// Random Obstacle selected
            float randomPos = default;
            if (gameObject.name != "EnvironmentCliff(L)(Clone)")
            {
                randomPos = Random.Range(transform.position.x + 0f, transform.position.x + 20f);// Random pos selected
            }
            else
            {
                randomPos = Random.Range(transform.position.x + 5f, transform.position.x + 20f);// Random pos selected
            }
            positions[i] = randomPos;
            for (int j = 0; j < i; j++)
            {
                if (Mathf.Abs(positions[j] - randomPos) > 3f)
                {
                    spawnable = true;
                }
                else
                {
                    spawnable = false;
                    i--;
                    break;

                }
            }
            if (spawnable)
            {
                Instantiate(obstacles[randomObstacle], new Vector3(randomPos, transform.position.y, 0f), transform.rotation, transform);
            }
        }    

        int numberOfSpecialObstacles = Random.Range(minimumSpecialObstacle, maximumSpecialObstacle + 1);
        for (int i = 0; i < numberOfSpecialObstacles; i++)
        {
            int randomSpecialObstacle = Random.Range(0, specialObstacles.Length);
            float randomPos = Random.Range(transform.position.x + 0f, transform.position.x + 20f);

            Instantiate(specialObstacles[randomSpecialObstacle], new Vector3(randomPos, transform.position.y, 0f), transform.rotation);
        }

        int numberOfEnvironmentDeoration = Random.Range(0, maximumEnvironmentDecoration);
        for (int i = 0; i < numberOfEnvironmentDeoration; i++)
        {
            int randomEnvironmentDecoration = Random.Range(0, environmentDecoration.Length);
            float randomPos = Random.Range(transform.position.x + 0f, transform.position.x + 20f);
            Instantiate(environmentDecoration[randomEnvironmentDecoration], new Vector3(randomPos, transform.position.y + Random.Range(3f, 4f), 0f), transform.rotation);
        }
    }

    private void Update()
    {
        if (player.position.x - transform.position.x > 30f)
        {
            Destroy(gameObject);
        }
    }
}
