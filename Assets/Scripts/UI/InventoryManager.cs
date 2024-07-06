using System;
using System.Collections;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Transform inventoryHolder;

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
            if (Input.GetKeyDown(KeyCode.E)) toggleInventory();
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



    private void toggleInventory()
    {
        if (inventoryHolder.gameObject.activeSelf) inventoryHolder.gameObject.SetActive(false);
        else inventoryHolder.gameObject.SetActive(true);
    }

    void ChangeSelectedSlot(int newSlot)
    {
        if (SelectedSlot >= 0) inventorySlots[SelectedSlot].Deselect();
        inventorySlots[newSlot].Select();
        SelectedSlot = newSlot;
    }
}
