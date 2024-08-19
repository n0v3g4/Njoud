using System.Collections;
using UnityEngine;

public class animation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Entity entity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        entity = GetComponent<Entity>();
    }
    // Update is called once per frame
    void Update()
    {
        if (entity.entityStats["animation"] == 0)
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
    }
    public IEnumerator Die()
    {
        animator.SetTrigger("die");
        //freeze rb
        rb.bodyType = RigidbodyType2D.Static;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        entity.barsHolder.SetActive(false);
        //in case the death animation fails to destroy the gameObject
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    //callde by the death animation
    public void delete()
    {
        Destroy(gameObject);
    }
}
