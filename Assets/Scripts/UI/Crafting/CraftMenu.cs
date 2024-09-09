using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class CraftMenu : MonoBehaviour
{
    [SerializeField] private Transform recipeContainer;
    public RecipeData[] recipeDatas;
    [SerializeField] private GameObject recipeDataPrefab;
    public InventoryManager inventoryManager;
    public MenuManager menuManager;
    [HideInInspector] public List<RecipeSlot> recipeSlots = new List<RecipeSlot>();
    [SerializeField] private GameObject itemDropPrefab;
    private int pickupDelay = 5;
    [SerializeField] private Transform playerPosition;

    void Awake()
    {
        for (int i = 0; i < recipeDatas.Length; i++)
        {
            GameObject recipeSlot = Instantiate(recipeDataPrefab);
            recipeSlot.transform.SetParent(recipeContainer);
            recipeSlot.GetComponent<RecipeSlot>().InitialiseRecipeSlot(recipeDatas[i], this);
            recipeSlots.Add(recipeSlot.GetComponent<RecipeSlot>());
        }
    }

    //turn the text red if the cost is not met
    public void UpdateSlotCosts()
    {
        for (int i = 0; i < recipeSlots.Count; i++)
        {
            UpdateSlotCost(recipeSlots[i]);
        }
    }
    public void UpdateSlotCost(RecipeSlot recipeSlot)
    {
        inventoryManager.updateItemDict();
        recipeSlot.costMet = true;
        for (int i = 0; i < recipeSlot.recipeCosts.Count; i++)
        {
            BuildCost buildCost = recipeSlot.recipeCosts[i];
            int itemCount = 0;
            if (inventoryManager.itemDict.ContainsKey(buildCost.item)) itemCount = inventoryManager.itemDict[buildCost.item];
            buildCost.SetCostText(buildCost.cost <= itemCount);
            recipeSlot.costMet = (recipeSlot.costMet && buildCost.cost <= itemCount);
        }
    }

    public void CraftSlotPressed(RecipeSlot recipeSlot)
    {
        if (recipeSlot.costMet)
        {
            inventoryManager.RemoveResourceCost(recipeSlot.recipeData.Costs);
            UpdateSlotCost(recipeSlot);
            int droppedItems = inventoryManager.AddItem(recipeSlot.recipeData.resultData, recipeSlot.recipeData.resultCount);
            if (droppedItems > 0) {
                GameObject itemDrop = Instantiate(itemDropPrefab, playerPosition.position, Quaternion.identity);
                itemDrop.GetComponent<ItemDrop>().InitialiseItem(recipeSlot.recipeData.resultData, recipeSlot.recipeData.resultCount, pickupDelay);
            }
        }
    }
}
