using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private bool isDead;

    public int health = 3;

    public bool isFront;
    private Vector2 direction;

    public bool isRight;
    public float stopDistance;

    public Transform point;
    public Transform behind;

    public float speed;
    public float maxVision;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (isRight) //vira pra direita
        {
            transform.eulerAngles = new Vector2(0, 0);
            direction = Vector2.right;
        }
        else //vira pra esquerda
        {
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.left;
        }
    }


    void FixedUpdate()
    {
        GetPlayer();

        OnMove();
    }

    void OnMove()
    {
        if(isFront && !isDead)
        {
            anim.SetInteger("transition", 1);

            if (isRight) //vira pra direita
            {
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.right;
                rig.velocity = new Vector2(speed, rig.velocity.y);
            }
            else //vira pra esquerda
            {
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.left;
                rig.velocity = new Vector2(-speed, rig.velocity.y);
            }
        }
  
    }

    void GetPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, direction, maxVision);

        if(hit.collider != null && !isDead)
        {
            if(hit.transform.CompareTag("Player"))
            {
                isFront = true;

                float distance = Vector2.Distance(transform.position, hit.transform.position);                  
                
                if(distance <= stopDistance) //distancia para atacar
                {
                    isFront = false;
                    rig.velocity = Vector2.zero;

                    anim.SetInteger("transition", 2);

                    hit.transform.GetComponent<Player>().OnHit();
                }                
            }
            else
            {
                isFront = false;
                rig.velocity = Vector2.zero;
                anim.SetInteger("transition", 0);
            }
        }

        RaycastHit2D behindHit = Physics2D.Raycast(behind.position, -direction, maxVision);

        if(behindHit.collider != null)
        {
            if(behindHit.transform.CompareTag("Player"))
            {
                //player está nas costas do inimigo
                isRight = !isRight;
                isFront = true;
            }
        }

    }

    public void OnHit()
    {
        anim.SetTrigger("hit");
        health--;

        if (health <= 0)
        {
            isDead = true;
            speed = 0;
            anim.SetTrigger("dead");
            Destroy(gameObject, 1f);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(point.position, direction * maxVision);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(behind.position, -direction * maxVision);
    }

}
