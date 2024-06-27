using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDetection : MonoBehaviour
{
    private enemy Enemy;
    private Transform currentTarget;

    private void Start()
    {
        Enemy = GetComponentInParent<enemy>();
    }

    //this seems a roundabout way of doing it, dont seem to have a choice though
    private void FixedUpdate()
    {
        this.transform.position = Enemy.Enemy.rb.transform.position;
    }

    //chase target till it leaves range
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy != 0 must hold true
        if(currentTarget == null)
        {
            entity Collider = collision.GetComponent<entity>();
            if (Collider != null)
            {
                float colliderTeam = -1;
                if (Collider.entityStats.TryGetValue("team", out float value)) colliderTeam = value;
                if (colliderTeam != Enemy.enemyTeam)
                {
                    currentTarget = Collider.rb.transform;
                    Enemy.changeTarget(currentTarget);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        entity Collider = collision.GetComponent<entity>();
        if (Collider != null && currentTarget == Collider.rb.transform)
        {
            currentTarget = null;
            Enemy.changeTarget(Enemy.Enemy.rb.transform);
        }
    }
}
