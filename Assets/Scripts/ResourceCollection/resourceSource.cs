using System;
using System.Collections;
using UnityEngine;

public class resourceSource : MonoBehaviour
{
    public int collectorTeam = 0; //the team allowed to collect resources
    public ActionType actionType = ActionType.Chop; //the tool required
    private float dropSpeed = 1f; //cooldown between drops
    private float pickupDelay = .5f;
    private bool dropCooldown = false;
    private float dropDistance = 1f;

    [SerializeField] private GridManager gridManager;
    [SerializeField] private Vector3Int size;
    [SerializeField] private itemData item; //the item that is dropped
    [SerializeField] private int count = 1;
    [SerializeField] private GameObject itemDropPrefab;

    public void Awake()
    {
        if (gridManager)
        {
            this.transform.position = gridManager.CenterGridPosition(this.transform.position, size);
            gridManager.blockTiles(this.transform.position, size);
        }
    }

    private void drop()
    {
        //create an item drop
        Vector3 spawnLocation = (UnityEngine.Random.insideUnitCircle.normalized * dropDistance);
        spawnLocation += transform.position;
        GameObject newItemGo = Instantiate(itemDropPrefab, spawnLocation, Quaternion.identity);
        newItemGo.GetComponent<ItemDrop>().InitialiseItem(item, count, pickupDelay);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Entity Collider = collision.GetComponent<Entity>();
        InventoryManager inventoryManager;
        try { inventoryManager = collision.GetComponent<Player>().inventoryManager; }
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

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dropDistance);
    }
}
