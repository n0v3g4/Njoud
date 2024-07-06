using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Sprite inventorySlot;
    public Sprite selectedInventorySlot; 

    private void Awake()
    {
        Deselect();
    }

    public void Select()
    {
        image.sprite = selectedInventorySlot;
    }
    public void Deselect()
    {
        image.sprite = inventorySlot;
    }

    //if an Item is dropped at a slot, set the new Parent to that slot. If the slot is full swap the items
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem droppedItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (transform.childCount != 0)
        {
            InventoryItem occupyingItem = transform.GetChild(0).gameObject.GetComponent<InventoryItem>();
            if (droppedItem.item == occupyingItem.item)
            {
                //if both items are the same try stacking them
                int emptySpace = occupyingItem.item.maxStack - occupyingItem.count;
                occupyingItem.count += Math.Min(emptySpace, droppedItem.count);
                occupyingItem.RefreshCount();
                droppedItem.count -= emptySpace;
                if (droppedItem.count <= 0) Destroy(droppedItem.gameObject);
                else droppedItem.RefreshCount();
                return;
            }
            else 
            {
                if (!droppedItem.originBlocked) occupyingItem.transform.SetParent(droppedItem.parentAfterDrag);
                else return;
            }
        }
        droppedItem.parentAfterDrag = transform;
    }
}
