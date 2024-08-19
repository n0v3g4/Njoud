using UnityEngine;

public class StraightLineEnemy : Enemy
{
    public override void Move()
    {
        Vector2 movement = target.transform.position - rb.transform.position;
        rb.AddForce(movement.normalized * entityStats["ms"]);
    }
}
