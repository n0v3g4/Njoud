using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Building")]

public class BuildingData : ScriptableObject
{
    public ResourceCosts[] Costs;
    public string description;
    public Vector3 size; //width and hight in grid

    public GameObject buildingPrefab;
}

[System.Serializable]
public struct ResourceCosts
{
    public int cost;
    public itemData item;
}
