using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable objecct/Item")]
public class itemData : ScriptableObject
{
    public int pickupTeam = 0;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(2, 2);

    public bool stackable;

    public Sprite image;
}

public enum ItemType
{
    Tool,
    Resource
}

public enum ActionType
{
    None,
    Chop,
    Mine
}