using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;

public class BasicTurret : MonoBehaviour
{
    public bool rotateTurret;
    private float range = 5f;
    private float rotationSpeed = 200f;
    private float fireDelay = 1f;
    private bool fireOnDelay = false;
    private int targetTeam = -1;
    private Dictionary<string, float> damageStats = new Dictionary<string, float>();
    public elementArray damage = new elementArray(new float[] { 10, 10, 10, 10 });
    private int bulletLifeTime = 5;

    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;

    private entity target;

    private void Start()
    {
        damageStats["knockback"] = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null) { findTarget(); }
        else 
        {
            if (rotateTurret) rotateTowardsTarget();
            if (!checkTargetInRange()) target = null;
            else if (!fireOnDelay) fire();
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

    private void fire()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().initializeBullet(direction, bulletLifeTime, targetTeam, damage, damageStats);
        fireOnDelay = true;
        StartCoroutine(fireCooldownReset());
    }

    private IEnumerator fireCooldownReset()
    {
        yield return new WaitForSeconds(fireDelay);
        fireOnDelay = false;
    }

}
