using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class itemData : ScriptableObject
{
    public int pickupTeam = 0;
    public int maxStack = 1;
    public ItemType type;
    public ActionType actionType; 

    public Sprite image;
}

public enum ItemType
{
    None,
    Tool,
    Resource
}

public enum ActionType
{
    None,
    Chop,
    Mine
}