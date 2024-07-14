using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class InventoryDropDetect : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject itemDropPrefab;
    [SerializeField] private Transform playerPosition;
    private float pickupDelay = 5f;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        //create an item drop
        GameObject newItemGo = Instantiate(itemDropPrefab, playerPosition.position, Quaternion.identity);
        newItemGo.GetComponent<ItemDrop>().InitialiseItem(droppedItem.item, droppedItem.count, pickupDelay);
        Destroy(droppedItem.gameObject);
    }
}
