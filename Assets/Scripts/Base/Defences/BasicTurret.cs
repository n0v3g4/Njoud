using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasicTurret : MonoBehaviour
{
    public bool rotateTurret;
    private float range = 5f;
    private float rotationSpeed = 200f;
    [SerializeField] private LayerMask enemyMask;


    private entity target;

    // Update is called once per frame
    void Update()
    {
        if (target == null) { findTarget(); }
        else 
        {
            if (rotateTurret) rotateTowardsTarget();
            if (!checkTargetInRange()) target = null;
        }
    }

    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);

        if(hits.Length > 0)
        {
            target = hits[0].transform.GetComponent<entity>();
        }
    }

    private void rotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 135f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);
    }

    private bool checkTargetInRange() { return Vector2.Distance(target.transform.position, transform.position) <= range; }

}
