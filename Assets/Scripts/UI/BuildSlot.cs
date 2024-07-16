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
    [SerializeField] public Image image;
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

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            //gets called if the Build Menu slot is rightclicked
            Debug.Log(image.sprite.name + " was clicked");
        }
    }
}
