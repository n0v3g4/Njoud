using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [HideInInspector] public Rigidbody2D target;
    [SerializeField] private LayerMask targetMask;
    public pathFinding pf;
    private float minimumTargetDistance = 0.01f;
    private bool attackCooldown = false;

    private void FixedUpdate()
    {
        if(target != null && Vector2.Distance(rb.transform.position, target.position) > minimumTargetDistance) pf.Move(target.position, rb, entityStats["ms"]);
    }

    private void Update()
    {
        if (!target) findTarget();
        else if (!checkTargetInRange()) target = null;
    }

    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, entityStats["range"], (Vector2)transform.position, 0f, targetMask);
        for(int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.GetComponent<Entity>().entityStats["team"] != entityStats["team"])
            {
                target = hits[i].transform.GetComponent<Rigidbody2D>();
                return;
            }
        }
    }
    private bool checkTargetInRange() { return Vector2.Distance(target.position, transform.position) <= entityStats["range"]; }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Entity Collider = collision.GetComponent<Entity>();
        if (Collider != null && !attackCooldown)
        {
            if (Collider.entityStats["team"] != entityStats["team"])
            {
                Vector2 collisionDirection = (collision.GetComponent<Transform>().position - rb.transform.position);
                damageStats["collisionDirectionX"] = collisionDirection.x;
                damageStats["collisionDirectionY"] = collisionDirection.y;
                Collider.Damage(entityElements["damage"], damageStats);
                attackCooldown = true;
                StartCoroutine(attackCooldownReset());
            }
        }
    }

    private IEnumerator attackCooldownReset()
    {
        yield return new WaitForSeconds(entityStats["as"]);
        attackCooldown = false;
    }

    //display range in editor
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, entityStats["range"]);
    }
}
