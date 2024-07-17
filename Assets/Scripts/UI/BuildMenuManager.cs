using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuManager : MonoBehaviour
{
    [SerializeField] private Transform buildingContainer;
    [SerializeField] private BuildingData[] buildingDatas;
    [SerializeField] private GameObject buildingDataPrefab;
    [SerializeField] private InventoryManager inventoryManager;
    [HideInInspector] public List<BuildCost> buildCosts = new List<BuildCost>();

    void Awake()
    {
        for(int i = 0; i < buildingDatas.Length; i++)
        {
            GameObject newItemGo = Instantiate(buildingDataPrefab);
            newItemGo.transform.SetParent(buildingContainer);
            newItemGo.GetComponent<BuildSlot>().InitialiseBuildSlot(buildingDatas[i], buildCosts, this);
        }
    }

    //turn the text red if the cost is not met
    public void UpdateSlotCost()
    {
        inventoryManager.updateItemDict();
        for(int i = 0; i < buildCosts.Count; i++)
        {
            if (inventoryManager.itemDict.ContainsKey(buildCosts[i].item)) buildCosts[i].SetCostText(buildCosts[i].cost <= inventoryManager.itemDict[buildCosts[i].item]);
            else buildCosts[i].SetCostText(false);
        }
    }

    public void BuildSlotPressed(BuildingData buildingData) 
    { 
    
    }

}
