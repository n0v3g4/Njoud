using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public entity Enemy;
    public Dictionary<string, float> damageStats = new Dictionary<string, float>();
    public elementArray damage = new elementArray(new float[] { 10, 10, 10, 10 });
    public Transform targetLocation;

    public float enemyTeam = -1;
    private float attackSpeed = 1; //cooldown between attacks in seconds
    private float minimumTargetDistance = 0.01f;
    private bool attackCooldown = false;
    public float damageScaling = 1;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponent<entity>();
        targetLocation = GetComponent<Rigidbody2D>().transform;
        if (Enemy.entityStats.TryGetValue("team", out float value)) enemyTeam = value;
        //base knockback prevents constant damage
        damageStats["knockback"] = 50;

        applySpesifics();

        for (int i = 0; i < damage.elements.Length; i++) damage.elements[i] *= damageScaling;
    }

    private void FixedUpdate()
    {
        if(targetLocation != null && Vector2.Distance(Enemy.rb.transform.position, targetLocation.position) > minimumTargetDistance) movement();
    }

    //apply enemyspesific things
    public virtual void applySpesifics()
    {
        { }
    }

    //the movement ai
    public virtual void movement()
    {
        { }
    }

    //gets called on death
    public virtual void onDeath()
    {
        { }
    } 

    public void changeTarget(Transform target)
    {
        targetLocation = target;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        entity Collider = collision.GetComponent<entity>();
        if (Collider != null && !attackCooldown)
        {
            float colliderTeam = -1;
            if (Collider.entityStats.TryGetValue("team", out float value)) colliderTeam = value;
            if (colliderTeam != enemyTeam)
            {
                Vector2 collisionDirection = (collision.GetComponent<Transform>().position - Enemy.rb.transform.position);
                damageStats["collisionDirectionX"] = collisionDirection.x;
                damageStats["collisionDirectionY"] = collisionDirection.y;
                Collider.Damage(damage, damageStats);
                attackCooldown = true;
                StartCoroutine(attackCooldownReset());
            }
        }
    }

    private IEnumerator attackCooldownReset()
    {
        yield return new WaitForSeconds(attackSpeed);
        attackCooldown = false;
    }
}
