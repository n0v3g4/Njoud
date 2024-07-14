using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemDrop : MonoBehaviour
{
    public SpriteRenderer image;
    public TextMeshPro countText;

    private float pickupAttemptDelay = 1f;
    private bool onDelay = false;

    [HideInInspector] public itemData item;
    [HideInInspector] public int count;

    public void InitialiseItem(itemData newItem, int Tcount, float pickupDelay)
    {
        item = newItem;
        count = Tcount;
        image.sprite = newItem.image;
        onDelay = true;
        RefreshCount();
        StartCoroutine(pickupDelayReset(pickupDelay));
    }
    public void RefreshCount()
    {
        countText.SetText(count.ToString());
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        entity Collider = collision.GetComponent<entity>();
        InventoryManager ColliderInventory;
        try { ColliderInventory = collision.GetComponent<playerStatsDefaults>().inventoryManager; }
        catch (Exception) { ColliderInventory = null; }
        if (Collider != null && !onDelay)
        {
            float colliderTeam = -1;
            if (Collider.entityStats.TryGetValue("team", out float value)) colliderTeam = value;
            if (colliderTeam == item.pickupTeam)
            {
                count = ColliderInventory.AddItem(item, count);
                if (count <= 0) Destroy(gameObject);
                else RefreshCount();
                onDelay = true;
                StartCoroutine(pickupDelayReset(pickupAttemptDelay));
            }
        }
    }

    private IEnumerator pickupDelayReset(float delay)
    {
        yield return new WaitForSeconds(delay);
        onDelay = false;
    }
}
