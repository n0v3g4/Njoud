using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceSource : MonoBehaviour
{
    public int collectorTeam = 0; //the team allowed to collect resources
    public ActionType actionType = ActionType.Chop; //the tool required
    private float dropSpeed = 1f; //cooldown between drops
    private float pickupDelay = .5f;
    private bool dropCooldown = false;
    private float spread = 2f;

    public itemData item; //the item that is dropped
    public int count = 1;
    public GameObject itemDropPrefab;

    private void drop()
    {
        //create an item drop
        Vector3 spawnLocation = transform.position + new Vector3(UnityEngine.Random.Range(-spread, spread), UnityEngine.Random.Range(-spread, spread), 0f);
        GameObject newItemGo = Instantiate(itemDropPrefab, spawnLocation, Quaternion.identity);
        newItemGo.GetComponent<ItemDrop>().InitialiseItem(item, count, pickupDelay);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        entity Collider = collision.GetComponent<entity>();
        InventoryManager inventoryManager;
        try { inventoryManager = collision.GetComponent<playerStatsDefaults>().inventoryManager; }
        catch (Exception) { inventoryManager = null; }
        //ColliderItem and ColliderActinos are potentally null, which results in an ActionType of None
        itemData ColliderItem = inventoryManager == null ? null : inventoryManager.GetSelectedItem(false);
        ActionType ColliderAction = (actionType == ActionType.None || ColliderItem == null) ? ActionType.None : ColliderItem.actionType;
        if (Collider != null && !dropCooldown)
        {
            float colliderTeam = -1;
            if (Collider.entityStats.TryGetValue("team", out float value)) colliderTeam = value;
            if (colliderTeam != collectorTeam) return;
            if (actionType == ColliderAction)
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
