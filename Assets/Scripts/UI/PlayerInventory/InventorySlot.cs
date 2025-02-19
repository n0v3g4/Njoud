using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;

    private Vector3 normalScale = new Vector3(1, 1, 1);
    private Vector3 selectedScaleScale = new Vector3(1.125f, 1.125f, 1);

    private void Awake()
    {
        Deselect();
    }

    public void Select() { image.transform.localScale = selectedScaleScale; }
    public void Deselect() { image.transform.localScale = normalScale; }

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
                occupyingItem.RefreshCount(occupyingItem.count + Math.Min(emptySpace, droppedItem.count));
                droppedItem.RefreshCount(droppedItem.count - emptySpace);
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
