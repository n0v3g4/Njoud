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

    public bool AddItem(itemData item)
    {
        //find empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    public void SpawnNewItem(itemData item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGo.GetComponent<InventoryItem>().InitialiseItem(item);
    }

    private void toggleInventory()
    {
        if (inventoryHolder.gameObject.activeSelf) inventoryHolder.gameObject.SetActive(false);
        else inventoryHolder.gameObject.SetActive(true);
    }
}
