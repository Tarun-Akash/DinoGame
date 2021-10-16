using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    Rigidbody2D spear;
    [SerializeField, Range(0f, 100f)] float speed = 10f;
    Vector2 velocity;
    GameObject dino;


    private void Start()
    {
        spear = GetComponent<Rigidbody2D>();
        dino = GameObject.Find("Dino");
    }

    private void FixedUpdate()
    {
        velocity.y = 0f;
        velocity.x = dino.GetComponent<Rigidbody2D>().velocity.x * 2f;
        spear.velocity = velocity;
        if(Vector3.Distance(this.transform.position,dino.transform.position) > 10f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Dino")
        {
            Destroy(gameObject);
        }
        else if(collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
