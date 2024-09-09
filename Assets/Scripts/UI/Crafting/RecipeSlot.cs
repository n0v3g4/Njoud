using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecipeSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject costPrefab;
    [SerializeField] private Transform costHolder;
    [SerializeField] private Image image;
    private CraftMenu craftMenu;
    [HideInInspector] public RecipeData recipeData;
    [HideInInspector] public bool costMet;
    //these are the displayed costs
    [HideInInspector] public List<BuildCost> recipeCosts = new List<BuildCost>();
    public void InitialiseRecipeSlot(RecipeData _recipeData, CraftMenu _craftMenu)
    {
        craftMenu = _craftMenu;
        recipeData = _recipeData;
        text.SetText(recipeData.description);
        image.sprite = recipeData.resultData.image;
        for (int i = 0; i < recipeData.Costs.Length; i++)
        {
            GameObject newItemCost = Instantiate(costPrefab);
            newItemCost.transform.SetParent(costHolder);
            newItemCost.GetComponent<BuildCost>().InitialiseBuildCost(recipeData.Costs[i].item, recipeData.Costs[i].cost);
            recipeCosts.Add(newItemCost.GetComponent<BuildCost>());
        }
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //gets called if the Build Menu slot is rightclicked
        if (pointerEventData.button == PointerEventData.InputButton.Left) craftMenu.CraftSlotPressed(this);
    }
}
