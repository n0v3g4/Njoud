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
    Resource,
    Tool,
    Weapon
}

public enum ActionType
{
    None,
    Chop,
    Mine,
    Cut
}