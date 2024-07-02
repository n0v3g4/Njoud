using System;
using System.Collections;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Transform inventoryHolder;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) toggleInventory();
    }

    public int AddItem(itemData item, int count)
    {
        //nothing to pick up
        if (count <= 0) return 0;
        //stack item if possible
        if (item.maxStack > 1)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < itemInSlot.item.maxStack)
                {
                    int emptySpace = itemInSlot.item.maxStack - itemInSlot.count;
                    itemInSlot.count += Math.Min(count, emptySpace);
                    itemInSlot.RefreshCount();
                    return AddItem(item, Math.Max(count - emptySpace, 0));
                }
            }
        }
        //find empty slot if possible
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot, Math.Min(count, item.maxStack));
                return AddItem(item, Math.Max(count - item.maxStack, 0));
            }
        }
        return count;
    }

    public void SpawnNewItem(itemData item, InventorySlot slot, int count)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGo.GetComponent<InventoryItem>().InitialiseItem(item, count);
    }

    private void toggleInventory()
    {
        if (inventoryHolder.gameObject.activeSelf) inventoryHolder.gameObject.SetActive(false);
        else inventoryHolder.gameObject.SetActive(true);
    }
}
