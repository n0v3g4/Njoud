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
            GameObject buildSlot = Instantiate(buildingDataPrefab);
            buildSlot.transform.SetParent(buildingContainer);
            buildSlot.GetComponent<BuildSlot>().InitialiseBuildSlot(buildingDatas[i], this);
            buildSlots.Add(buildSlot.GetComponent<BuildSlot>());
        }
    }

    //turn the text red if the cost is not met
    public void UpdateSlotCosts()
    {
        for(int i = 0; i < buildSlots.Count; i++)
        {
            UpdateSlotCost(buildSlots[i]);
        }
    }
    public void UpdateSlotCost(BuildSlot buildSlot)
    {
        inventoryManager.updateItemDict();
        buildSlot.costMet = true;
        for (int i = 0; i < buildSlot.buildCosts.Count; i++)
        {
            BuildCost buildCost = buildSlot.buildCosts[i];
            int itemCount = 0;
            if (inventoryManager.itemDict.ContainsKey(buildCost.item)) itemCount = inventoryManager.itemDict[buildCost.item];
            buildCost.SetCostText(buildCost.cost <= itemCount);
            buildSlot.costMet = (buildSlot.costMet && buildCost.cost <= itemCount);
        }
    }

    public void BuildSlotPressed(BuildSlot buildSlot) 
    {
        if (buildSlot.costMet)
        {
            buildMode.Build(buildSlot);
        }
    }
}
