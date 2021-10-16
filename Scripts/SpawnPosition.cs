using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    public Transform playerPosition;   
    Vector3 offset;

    private void Start()
    {
        offset = transform.position - playerPosition.position;
    }
    void Update()
    {
        transform.position = playerPosition.position + offset;
    }
}
