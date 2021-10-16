using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnvBlock
{
    public GameObject environment;
    public Vector3 startPos;
    public Vector3 endPos;
}

public class EnvironmentManager : MonoBehaviour
{
    public GameObject[] environment;
    private GameObject environmentToBeSpawned;
    public GameObject[] environmentCliffs;
    public Transform spawnPosition;
    bool cliff = false;
    bool empty = false;
    int i = 0;    
    //public GameObject initialEnvironment;

    private void Start()
    {
        spawnPosition.position = new Vector3(spawnPosition.position.x + 20f, -0.12f, 0f);        
    }

    public void SpawnEnvironment()
    {
        if (!cliff)
        {
            i = Random.Range(0, environment.Length);//Select RandomEnvironment
            environmentToBeSpawned = environment[i];
        }
        else
        {
            if (!empty)
            {
                i = 0;
            }
            else
            {
                i = Random.Range(0, environmentCliffs.Length);
            }
            environmentToBeSpawned = environmentCliffs[i];
        }
        GameObject spawnedEnvironment = Instantiate(environmentToBeSpawned, spawnPosition.position, transform.rotation);//Spawn and assign Environment
        Debug.Log("Spawned");
        spawnPosition.position = new Vector3(spawnedEnvironment.transform.position.x + 20f, -0.12f, 0f);//New Spawn Position
        
       
        if(spawnedEnvironment.gameObject.name == "EnvironmentCliff(R)(Clone)" || spawnedEnvironment.gameObject.name == "EnvironmentNoGround(Clone)")
        {
            cliff = true;
            if(spawnedEnvironment.gameObject.name == "EnvironmentCliff(R)(Clone)")
            {
                empty = false;
            }
            else
            {
                empty = true;
            }
        }
        else
        {
            cliff = false;
        }
    }
}
