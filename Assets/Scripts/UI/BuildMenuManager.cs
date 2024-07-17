using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuManager : MonoBehaviour
{
    [SerializeField] private Transform buildingContainer;
    [SerializeField] private BuildingData[] buildingDatas;
    [SerializeField] private GameObject buildingDataPrefab;
    public List<BuildCost> buildCosts = new List<BuildCost>();

    void Awake()
    {
        for(int i = 0; i < buildingDatas.Length; i++)
        {
            GameObject newItemGo = Instantiate(buildingDataPrefab);
            newItemGo.transform.SetParent(buildingContainer);
            newItemGo.GetComponent<BuildSlot>().InitialiseBuildSlot(buildingDatas[i], buildCosts);
        }
    }

    //turn the text red if the cost is not met
    public void UpdateSlotCost()
    {

    }

    public void BuildSlotPressed(BuildingData buildingData) 
    { 
    
    }

}
