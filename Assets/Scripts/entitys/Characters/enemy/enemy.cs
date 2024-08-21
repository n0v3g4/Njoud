using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [HideInInspector] public Entity target;
    [SerializeField] private LayerMask targetMask;
    public pathFinding pf;
    private float detectionRange = 5;
    private float minimumTargetDistance = 0.01f;
    private bool attackCooldown = false;

    private void FixedUpdate()
    {
        if(target != null && Vector2.Distance(rb.transform.position, target.transform.position) > minimumTargetDistance) pf.Move(target.transform, rb, entityStats["ms"]);
    }

    private void Update()
    {
        if (!target) findTarget();
        else if (!checkTargetInRange()) target = null;
    }

    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, detectionRange, (Vector2)transform.position, 0f, targetMask);
        if (hits.Length > 0) { target = hits[0].transform.GetComponent<Entity>();  }
    }
    private bool checkTargetInRange() { return Vector2.Distance(target.transform.position, transform.position) <= detectionRange; }

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
}
