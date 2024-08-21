using UnityEngine;

public class pathFinding : MonoBehaviour
{
    public virtual void Move(Vector3 target, Rigidbody2D rb, float ms)
    {
        Vector2 movement = target - rb.transform.position;
        rb.AddForce(movement.normalized * ms);
    }
}
