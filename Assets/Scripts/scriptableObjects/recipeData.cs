using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Recipes")]

public class RecipeData : ScriptableObject
{
    public ResourceCosts[] Costs; //see "BuildingData.cs"
    public string description;

    public int resultCount;
    public itemData resultData;
}
