using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuManager : MonoBehaviour
{
    [SerializeField] private Transform buildingContainer;
    [SerializeField] private BuildingData[] buildingDatas;
    [SerializeField] private GameObject buildingDataPrefab;

    void Awake()
    {
        for(int i = 0; i < buildingDatas.Length; i++)
        {
            GameObject newItemGo = Instantiate(buildingDataPrefab);
            newItemGo.transform.SetParent(buildingContainer);
            newItemGo.GetComponent<BuildSlot>().InitialiseBuildSlot(buildingDatas[i]);
        }
    }
}
