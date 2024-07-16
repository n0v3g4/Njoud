using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Building")]

public class BuildingData : ScriptableObject
{
    public BuildingCost[] Costs;
    public string description;

    public GameObject buildingPrefab;
}

[System.Serializable]
public struct BuildingCost
{
    public int cost;
    public itemData item;
}
