using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    Rigidbody2D player;
    float speed;
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.SetParent(null);
        player = GameObject.Find("Dino").GetComponent<Rigidbody2D>();
        speed = player.velocity.x;
    }   
    void FixedUpdate()
    {
        if (!gameManager.gameOver)
        {
            transform.position = new Vector3((transform.position.x) + ((speed - 1) * Time.deltaTime), transform.position.y, transform.position.z);
        }
        if(player.transform.position.x - transform.position.x > 10f)
        {
            Destroy(gameObject);
        }
    }
}
