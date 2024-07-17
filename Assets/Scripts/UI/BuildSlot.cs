using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject costPrefab;
    [SerializeField] private Transform costHolder;
    [SerializeField] private Image image;
    private BuildMenuManager buildMenuManager;
    private BuildingData buildingData;
    public void InitialiseBuildSlot(BuildingData _buildingData, List<BuildCost> buildCosts, BuildMenuManager _buildMenuManager)
    {
        buildMenuManager = _buildMenuManager;
        buildingData = _buildingData;
        text.SetText(buildingData.description);
        image.sprite = buildingData.buildingPrefab.GetComponent<SpriteRenderer>().sprite;
        for (int i = 0; i < buildingData.Costs.Length; i++)
        {
            GameObject newItemGo = Instantiate(costPrefab);
            newItemGo.transform.SetParent(costHolder);
            newItemGo.GetComponent<BuildCost>().InitialiseBuildCost(buildingData.Costs[i].item, buildingData.Costs[i].cost);
            buildCosts.Add(newItemGo.GetComponent<BuildCost>());
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //gets called if the Build Menu slot is rightclicked
        if (pointerEventData.button == PointerEventData.InputButton.Left) buildMenuManager.BuildSlotPressed(buildingData);
    }
}
