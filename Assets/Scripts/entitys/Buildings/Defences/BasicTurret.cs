using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : MonoBehaviour
{
    [SerializeField] private Entity entity;
    public bool rotateTurret;
    private float rotationSpeed = 200f;
    private bool fireOnDelay = false;
    private int bulletLifeTime = 5;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;

    private Entity target;

    // Update is called once per frame
    void Update()
    {
        if (!target) findTarget();
        else 
        {
            if (rotateTurret) rotateTowardsTarget();
            if (!checkTargetInRange()) target = null;
            else if (!fireOnDelay) fire();
        }
    }

    private void findTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, entity.entityStats["range"], (Vector2)transform.position, 0f, targetMask);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.GetComponent<Entity>().entityStats["team"] != entity.entityStats["team"])
            {
                target = hits[0].transform.GetComponent<Entity>();
                return;
            }
        }
    }

    private void rotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 135f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);
    }

    private bool checkTargetInRange() { return Vector2.Distance(target.transform.position, transform.position) <= entity.entityStats["range"]; }

    private void fire()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().initializeBullet(direction, bulletLifeTime, entity.entityStats["team"], entity.entityElements["damage"], entity.damageStats);
        fireOnDelay = true;
        StartCoroutine(fireCooldownReset());
    }

    private IEnumerator fireCooldownReset()
    {
        yield return new WaitForSeconds(entity.entityStats["as"]);
        fireOnDelay = false;
    }

}
