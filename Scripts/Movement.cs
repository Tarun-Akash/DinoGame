using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D unitRb;
    [SerializeField, Range(0f, 100f)] float speedX = 5f;
    Vector2 velocity;
    EnvironmentManager envManager;
    [SerializeField, Range(0f, 100f)] private float gravity = 10f;
    [SerializeField, Range(0f, 100f)] private float height = 5f;
    private bool onGround;
    private bool jump;
    private bool wings;
    bool powerUpRoutine;
    Animator anim;
    bool flying;
    Vector3 gravityValue;
    bool isColliding;

    [SerializeField] int health = 3;

    float groundLevel;

    GameManager gameManager;
    private bool hasFlyingPowerUp;

    private void Start()
    {
        unitRb = GetComponent<Rigidbody2D>();
        envManager = GameObject.Find("EnvironmentManager").GetComponent<EnvironmentManager>();
        groundLevel = transform.position.y;
        anim = GetComponent<Animator>();
        Physics2D.gravity *= 3f;
        gravityValue = Physics2D.gravity;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if(gameManager.gameOver)
        {
            unitRb.isKinematic = true;
        }
        speedX += (speedX * 0.001f * Time.deltaTime);
        jump |= Input.GetKeyDown(KeyCode.Space);
        wings |= Input.GetKeyDown(KeyCode.W);

        if(onGround && gameManager.hasGameStarted && !gameManager.gameOver)
        {
            anim.enabled = true;
        }
        else
        {
            anim.enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            hasFlyingPowerUp = !hasFlyingPowerUp;
        }
    }
    private void FixedUpdate()
    {        
        velocity = unitRb.velocity;       
        velocity.x = speedX;      
        if(jump && onGround)
        {
            Jump();
            jump = false;
        }
        if(flying)
        {           
            velocity.y = Mathf.Sin(transform.position.x) * 0.2f;
            if(jump)
            {
                Jump();
                jump = false;
            }
        }
        unitRb.velocity = velocity;       
        onGround = false;
        if(!gameManager.hasGameStarted || gameManager.gameOver)
        {
            unitRb.velocity = Vector2.zero;
        }          
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            if (isColliding) return;
            isColliding = true;
            envManager.SpawnEnvironment();
            StartCoroutine(Reset());
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameManager.gameOver = true;
        }
       
        //POWER UPS
        if(collision.gameObject.CompareTag("PowerUp"))
        {
            if(collision.gameObject.name == "DinoLeaf")
            {
                Destroy(collision.gameObject);
                hasFlyingPowerUp = true;
            }
        }

        if (collision.gameObject.name == "NoFlyZone")
        {
            if (flying)
            {
                flying = false;
                hasFlyingPowerUp = false;
                transform.Find("DinoLeaf").gameObject.SetActive(false);
                Physics2D.gravity = gravityValue;
            }
        }

        if (collision.gameObject.name == "Abyss")
        {
            if (!flying)
            {
                gameManager.gameOver = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "FlyZone")
        {
            if (hasFlyingPowerUp)
            {
                transform.Find("DinoLeaf").gameObject.SetActive(true);
                flying = true;
                Physics2D.gravity = Vector3.zero;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

    }


    public void Jump()
    {        
        velocity.y += Mathf.Sqrt(-2 * Physics2D.gravity.y * height);
        onGround = false;        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {      
        onGround = true;
        jump = false;
    }



     IEnumerator DisablePowerUpAfterSeconds(float seconds)
    {        
        yield return new WaitForSeconds(3);
        wings = false;
        powerUpRoutine = false;
    }


    public void Obstacled()
    {
        health--;        
    }
 

    IEnumerator Reset()
    {        
        yield return new WaitForSeconds(1f);
        isColliding = false;
    }
}
