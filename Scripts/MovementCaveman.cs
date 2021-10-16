using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCaveman : MonoBehaviour
{
    Rigidbody2D unitRb;
    //[SerializeField, Range(0f, 100f)] float speed = 5f;
    Vector2 velocity;
    public GameObject spear;
    Rigidbody2D player;
    GameManager gameManager;
    Animator anim;
    bool freefall;

    private void Start()
    {
        unitRb = GetComponent<Rigidbody2D>();
        oldt = Time.time;
        player = GameObject.Find("Dino").GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        anim = GetComponent<Animator>();
    }

    float oldt;
    

    private void Update()
    {
        if (!gameManager.gameOver)
        {
            float t = Time.time;
            if (t > oldt)
            {
                int i = Random.Range(1, 101);
                if (i > 50)
                {
                    Instantiate(spear, transform.position, transform.rotation);
                }
                oldt += 2f;
            }
        }
        else
        {
            anim.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        velocity.y = 0f;
        velocity.x = velocity.x == 0f ? player.velocity.x : velocity.x - (0.1f * Time.deltaTime);

        velocity.x = freefall ? velocity.x * 0.9f : velocity.x;
        velocity.y = freefall ? -5f : 0f;

        unitRb.velocity = velocity;
        if (!gameManager.hasGameStarted || gameManager.gameOver)
        {
            unitRb.velocity = Vector2.zero;
        }        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "FlyZone")
        {
            freefall = true;
        }
    }
}
