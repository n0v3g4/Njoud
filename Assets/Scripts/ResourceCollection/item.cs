//using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class item : MonoBehaviour
{

    private int collectorTeam = 0; //the team which can collect this
    private float spread = 1f;
    public string itemName = "wood";
    private int itemCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(string tempItemName)
    {
        //set the location to a random range around the spawner
        transform.position += new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0f);
        itemName = tempItemName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        entity Collider = collision.GetComponent<entity>();
        if (Collider != null)
        {
            float colliderTeam = -1;
            if (Collider.entityStats.TryGetValue("team", out float value)) colliderTeam = value;
            if (colliderTeam == collectorTeam)
            {
                playerInventory inventory = collision.GetComponent<playerInventory>();
                bool tryAdding = inventory.pickup(itemName, itemCount);
                if (tryAdding) Destroy(gameObject);
            }
        }
    }
}
