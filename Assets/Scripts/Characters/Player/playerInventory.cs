using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class playerInventory : MonoBehaviour
{
    private int maxItems = 16;
    //stores items as (name, count)
    public Dictionary<string, int> storedItems = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //called on item pickup
    public bool pickup(string name, int count)
    {
        //create new key if item isnt in store already
        if (!this.storedItems.TryGetValue(name, out int value))
        {
            if (!(storedItems.Count >= maxItems)) this.storedItems[name] = count;
            //return false if inventory is full
            else return false;
        }
        else this.storedItems[name] += count;
        Debug.Log("added wood");
        Debug.Log(this.storedItems[name]);
        return true;
    }
}
