using System.Collections;
using UnityEngine;
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

    public void InitialiseItem(itemData newItem, int _count, float pickupDelay)
    {
        item = newItem;
        image.sprite = newItem.image;
        onDelay = true;
        RefreshCount(_count);
        StartCoroutine(pickupDelayReset(pickupDelay));
    }
    public void RefreshCount(int _count)
    {
        count = _count;
        if (count <= 0) Destroy(gameObject);
        countText.SetText(count.ToString());
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Entity Collider = collision.GetComponent<Entity>();
        InventoryManager ColliderInventory;
        try { ColliderInventory = collision.GetComponent<Player>().inventoryManager; }
        catch (Exception) { ColliderInventory = null; }
        if (ColliderInventory != null && !onDelay)
        {
            if (Collider.entityStats["team"] == item.pickupTeam)
            {
                int _count = ColliderInventory.AddItem(item, count);
                RefreshCount(_count);
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
