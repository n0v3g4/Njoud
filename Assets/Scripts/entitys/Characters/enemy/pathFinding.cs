using UnityEngine;

public class pathFinding : MonoBehaviour
{
    public virtual void Move(Transform target, Rigidbody2D rb, float ms)
    {
        Vector2 movement = target.position - rb.transform.position;
        rb.AddForce(movement.normalized * ms);
    }
}
