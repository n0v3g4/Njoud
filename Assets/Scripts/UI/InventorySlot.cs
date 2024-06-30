using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    //if an Item is dropped at a slot, set the new Parent to that slot. If the slot is full swap the items
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();
        if (transform.childCount != 0)
        {
            GameObject current = transform.GetChild(0).gameObject;
            InventoryItem currentInventoryItem = current.GetComponent<InventoryItem>();
            currentInventoryItem.transform.SetParent(inventoryItem.parentAfterDrag);
        }
        inventoryItem.parentAfterDrag = transform;
    }
}
