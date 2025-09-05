using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtom : MonoBehaviour
{
    private Animator anim;
    public Animator barrierAnim;

    public LayerMask layer;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnPressed()
    {
        anim.SetBool("isPressed", true);
        barrierAnim.SetBool("isPressed", true);
    }

    void OnExit()
    {
        anim.SetBool("isPressed", false);
        barrierAnim.SetBool("isPressed", false);
    }

    ////retorna enquanto um objeto está em colisão com outro
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Stone"))
    //    {
    //        OnPressed();
    //    }
    //}

    ////retorna quando um objeto sai de colisão com outro
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Stone"))
    //    {
    //        OnExit();
    //    }
    //}

    private void FixedUpdate()
    {
        OnCollision();
    }

    void OnCollision()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 1, layer);

        if (hit != null)
        {
            OnPressed();
            hit = null;
        }
        else
        {
            OnExit();
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
