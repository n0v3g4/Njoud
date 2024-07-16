using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject costPrefab;
    [SerializeField] private Transform costHolder;
    [SerializeField] private Image image;
    public void InitialiseBuildSlot(BuildingData buildingData)
    {
        text.SetText(buildingData.description);
        image.sprite = buildingData.buildingPrefab.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < buildingData.Costs.Length; i++)
        {
            GameObject newItemGo = Instantiate(costPrefab);
            newItemGo.transform.SetParent(costHolder);
            newItemGo.GetComponent<BuildCost>().InitialiseBuildCost(buildingData.Costs[i].item.image, buildingData.Costs[i].cost);
        }
    }
}
