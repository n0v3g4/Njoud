using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Vector2 direction;
    private Dictionary<string, float> damageStats = new Dictionary<string, float>(); //basic values
    public elementArray damage = new elementArray(new float[] { 1, 1, 1, 1 });
    private float bulletSpeed = 5f;
    private float team = 0;
    private int lifeTime = 5;

    private bool hit = false;

    public void initializeBullet(Vector2 _direction, int _lifeTime, float _team, elementArray _damage, Dictionary<string, float> _damageStats) { 
        direction = _direction;
        lifeTime = _lifeTime;
        team = _team;
        damageStats = _damageStats;
        damage = _damage;
        StartCoroutine(dieAfterLifeTime());
    }

    private void FixedUpdate()
    {
        if (direction == null) return;
        rb.linearVelocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity Collider = collision.GetComponent<Entity>();
        if (Collider != null && !hit)
        {
            if (Collider.entityStats["team"] != team)
            {
                hit = true;
                Vector2 collisionDirection = (collision.GetComponent<Transform>().position - rb.transform.position);
                damageStats["collisionDirectionX"] = collisionDirection.x;
                damageStats["collisionDirectionY"] = collisionDirection.y;
                Collider.Damage(damage, damageStats);
                Destroy(gameObject);
            }
        }
    }
    private IEnumerator dieAfterLifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
