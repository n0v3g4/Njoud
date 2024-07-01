using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    public SpriteRenderer image;

    private float pickupDelay = 1f;
    private bool onDelay = false;

    [HideInInspector] public itemData item;
    [HideInInspector] public int count = 1;

    public void InitialiseItem(itemData newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        onDelay = true;
        StartCoroutine(pickupDelayReset());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        entity Collider = collision.GetComponent<entity>();
        InventoryManager ColliderInventory = collision.GetComponent<InventoryManager>();
        if (Collider != null && !onDelay)
        {
            float colliderTeam = -1;
            if (Collider.entityStats.TryGetValue("team", out float value)) colliderTeam = value;
            if (colliderTeam == item.pickupTeam)
            {
                bool tryAdding = ColliderInventory.AddItem(item, count);
                if (tryAdding) Destroy(gameObject);
                onDelay = true;
                StartCoroutine(pickupDelayReset());
            }
        }
    }

    private IEnumerator pickupDelayReset()
    {
        yield return new WaitForSeconds(pickupDelay);
        onDelay = false;
    }
}
