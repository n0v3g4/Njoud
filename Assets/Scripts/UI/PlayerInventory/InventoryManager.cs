using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Transform inventoryHolder;

    [HideInInspector] public Dictionary<itemData, int> itemDict = new Dictionary<itemData, int>();

    int SelectedSlot = -1;
    private float mouseScroll = 0;

    private void Start()
    {
        ChangeSelectedSlot(0);
    }

    //definition of % function that works for negative numbers
    int mod(int x, int m) { return (x % m + m) % m; }

    void Update()
    {
        if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 9)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
        mouseScroll = (Input.GetAxis("Mouse ScrollWheel"));
        if (mouseScroll < 0) ChangeSelectedSlot(mod((SelectedSlot - 1), 8));
        else if (mouseScroll > 0) ChangeSelectedSlot(mod((SelectedSlot + 1), 8));
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
                    itemInSlot.RefreshCount(itemInSlot.count + Math.Min(count, emptySpace));
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

    public void RemoveResourceCost(ResourceCosts[] resourceCosts)
    {
        for (int i = 0; i < resourceCosts.Length; i++) { RemoveItem(resourceCosts[i].item, resourceCosts[i].cost); }
    }

    public void RemoveItem(itemData item, int count)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item)
            {
                if(itemInSlot.count < count)
                {
                    count -= itemInSlot.count;
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount(itemInSlot.count - count);
                    return;
                }
            }
        }
        //this is reached if  more items are removed than possible (shouldnt happen)
    }

    public void updateItemDict()
    {
        itemDict.Clear();
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItem itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItem>();
            if (itemInSlot)
            {
                if (itemDict.ContainsKey(itemInSlot.item)) itemDict[itemInSlot.item] += itemInSlot.count;
                else itemDict[itemInSlot.item] = itemInSlot.count;
            }
        }
    }

    public void SpawnNewItem(itemData item, InventorySlot slot, int count)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGo.GetComponent<InventoryItem>().InitialiseItem(item, count, this);
    }

    public itemData GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[SelectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            itemData item = itemInSlot.item;
            if (use)
            {
                //do item using stuff if you want
            }
            return item;
        }
        return null;
    }

    void ChangeSelectedSlot(int newSlot)
    {
        if (SelectedSlot >= 0) inventorySlots[SelectedSlot].Deselect();
        inventorySlots[newSlot].Select();
        SelectedSlot = newSlot;
    }
}
