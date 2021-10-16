using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavemanCrushable : MonoBehaviour
{
    Animator anim;
    bool attack = false;
    GameObject player;
    GameObject child;
    bool skipped = false;

    private void Start()
    {        
        anim = GetComponent<Animator>();
        child = this.transform.Find("Caveman").gameObject;
        player = GameObject.Find("Dino");
        transform.SetParent(null);
        
        //anim.enabled = false;
    }

    private void Update()
    {
        if ((player != null && Vector3.Distance(player.transform.position, transform.position) > 5f && attack && !skipped) || skipped)
        {            
            anim.SetBool("isAttacking", true);
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                child.SetActive(true);
            }
            if (Vector3.Distance(player.transform.position, child.transform.position) > 11f)
            {
                Destroy(gameObject);
            }
        }
        if (player.transform.position.x - transform.position.x > 5f)
        {
            skipped = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Dino")
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                Vector2 normal = collision.GetContact(i).normal;
                if (!(normal.y <= 0f && normal.y >= -0.9f))
                {                    
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(0f, 5f);
                    anim.SetBool("isCrushed", true);
                    //anim.enabled = true;
                }
                else
                {//////////////////SET BOOL 
                    collision.gameObject.GetComponent<Movement>().Obstacled();
                    gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                    player = collision.gameObject;
                    attack = true;
                }
            }

        }
    }


}
