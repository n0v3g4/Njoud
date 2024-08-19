using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Building")]

public class BuildingData : ScriptableObject
{
    public BuildingCost[] Costs;
    public string description;
    public Vector3 size; //width and hight in grid

    public GameObject buildingPrefab;
}

[System.Serializable]
public struct BuildingCost
{
    public int cost;
    public itemData item;
}
