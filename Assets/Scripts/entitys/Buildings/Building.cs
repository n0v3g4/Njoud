using System.Numerics;
using UnityEngine;

public class Building : Entity
{
    private GridManager gridManager;
    private UnityEngine.Vector3 center;
    private UnityEngine.Vector3 size;

    //initialise the building
    public void Setup(GridManager _gridmanager, UnityEngine.Vector3 _center, UnityEngine.Vector3 _size)
    {
        gridManager = _gridmanager;
        center = _center;
        size = _size;
    }

    //remove the blocked squares
    public override void deathSpesifics()
    {
        gridManager.clearTiles(center, size);
    }
}
