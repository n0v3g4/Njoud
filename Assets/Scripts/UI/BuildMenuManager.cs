using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuManager : MonoBehaviour
{
    [SerializeField] private Transform buildingContainer;
    public BuildingData[] buildingDatas;
    [SerializeField] private GameObject buildingDataPrefab;
    public InventoryManager inventoryManager;
    public MenuManager menuManager;
    [SerializeField] private BuildMode buildMode;
    [HideInInspector] public List<BuildSlot> buildSlots = new List<BuildSlot>();

    void Awake()
    {
        for(int i = 0; i < buildingDatas.Length; i++)
        {
            GameObject newItemGo = Instantiate(buildingDataPrefab);
            newItemGo.transform.SetParent(buildingContainer);
            newItemGo.GetComponent<BuildSlot>().InitialiseBuildSlot(buildingDatas[i], this);
            buildSlots.Add(newItemGo.GetComponent<BuildSlot>());
        }
    }

    //turn the text red if the cost is not met
    public void UpdateSlotCost()
    {
        inventoryManager.updateItemDict();
        for(int i = 0; i < buildSlots.Count; i++)
        {
            buildSlots[i].costMet = true;
            for (int j = 0; j < buildSlots[i].buildCosts.Count; j++)
            {
                BuildCost buildCost = buildSlots[i].buildCosts[j];
                int itemCount = 0;
                if (inventoryManager.itemDict.ContainsKey(buildCost.item)) itemCount = inventoryManager.itemDict[buildCost.item];
                buildCost.SetCostText(buildCost.cost <= itemCount);
                buildSlots[i].costMet = buildCost.cost <= itemCount;
            }
        }
    }

    public void BuildSlotPressed(BuildingData buildingData, bool costMet) 
    {
        if (costMet)
        {
            
            buildMode.Build(buildingData);
        }
    }
}
