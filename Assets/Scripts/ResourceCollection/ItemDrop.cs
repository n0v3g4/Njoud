using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ItemDrop : MonoBehaviour
{
    public SpriteRenderer image;

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
        StartCoroutine(pickupDelayReset(pickupDelay));
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
                int tryAdding = ColliderInventory.AddItem(item, count);
                if (tryAdding <= 0) Destroy(gameObject);
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
