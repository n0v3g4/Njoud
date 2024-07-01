using System.Collections;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxStack = 4;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Transform inventoryHolder;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) toggleInventory();
    }

    public bool AddItem(itemData item, int itemCount)
    {
        //stack item if possible
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.itemCount < maxStack)
            { 
                itemInSlot.itemCount += itemCount;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        //find empty slot if possible
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot, itemCount);
                return true;
            }
        }
        return false;
    }

    public void SpawnNewItem(itemData item, InventorySlot slot, int itemCount)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGo.GetComponent<InventoryItem>().InitialiseItem(item, itemCount);
    }

    private void toggleInventory()
    {
        if (inventoryHolder.gameObject.activeSelf) inventoryHolder.gameObject.SetActive(false);
        else inventoryHolder.gameObject.SetActive(true);
    }
}
