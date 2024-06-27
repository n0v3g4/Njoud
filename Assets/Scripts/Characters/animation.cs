using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private entity Entity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        Entity = GetComponent<entity>();
        //prevents buggy initialisation
        animator.SetTrigger("moveDown");
    }
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) < Mathf.Abs(rb.velocity.y))
        {
            //if only y-movement play those animations
            if (rb.velocity.y > 0) animator.SetTrigger("moveUp");
            else animator.SetTrigger("moveDown");
        }
        else if (rb.velocity.x != 0)
        {
            //if x movement play it
            if (rb.velocity.x > 0) animator.SetTrigger("moveRight");
            else animator.SetTrigger("moveLeft");
        }
        else
        {
            //play idle animatinos
            animator.SetTrigger("idle");
        }
    }
    public IEnumerator Die()
    {
        animator.SetTrigger("die");
        //freeze rb
        rb.bodyType = RigidbodyType2D.Static;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Entity.barsHolder.SetActive(false);
        //called in case the death animation failed to delete itself
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    //calld on death animation finish
    public void delete()
    {
        Destroy(gameObject);
    }
}
