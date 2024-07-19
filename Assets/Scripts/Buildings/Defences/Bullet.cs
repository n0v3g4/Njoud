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
    private int targetTeam = -1;
    private int lifeTime = 5;

    private bool hit = false;

    public void initializeBullet(Vector2 _direction, int _lifeTime, int _targetTeam, elementArray _damage, Dictionary<string, float> _damageStats) { 
        direction = _direction;
        lifeTime = _lifeTime;
        targetTeam = _targetTeam;
        damageStats = _damageStats;
        damage = _damage;
        StartCoroutine(dieAfterLifeTime());
    }

    private void FixedUpdate()
    {
        if (direction == null) return;
        rb.velocity = direction * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        entity Collider = collision.GetComponent<entity>();
        if (Collider != null && !hit)
        {
            float colliderTeam = -1;
            if (Collider.entityStats.TryGetValue("team", out float value)) colliderTeam = value;
            if (colliderTeam == targetTeam)
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
