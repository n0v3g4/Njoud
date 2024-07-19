using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Grid grid;
    public Tilemap tilemap;
    [SerializeField] GameObject occupiedTilePrefab;
}
