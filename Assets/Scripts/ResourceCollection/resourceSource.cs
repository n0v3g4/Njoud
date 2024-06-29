using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class resourceSource : MonoBehaviour
{

    public int collectorTeam = 0; //the team of the entity that can collect this
    public string resourceName = "wood";
    private float dropSpeed = 1f; //cooldown between drops
    private bool dropCooldown = false;
    [SerializeField] private Transform resourcePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setName(string name)
    {
        resourceName = name;
    }

    private void drop()
    {
        //create an item drop
        Transform resourceDrop = Instantiate(resourcePrefab, transform.position, Quaternion.identity);
        resourceDrop.GetComponent<item>().Setup(resourceName);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        entity Collider = collision.GetComponent<entity>();
        if (Collider != null && !dropCooldown)
        {
            float colliderTeam = -1;
            if (Collider.entityStats.TryGetValue("team", out float value)) colliderTeam = value;
            if (colliderTeam == collectorTeam)
            {
                drop();
                dropCooldown = true;
                StartCoroutine(dropCooldownReset());
            }
        }
    }

    private IEnumerator dropCooldownReset()
    {
        yield return new WaitForSeconds(dropSpeed);
        dropCooldown = false;
    }
}
