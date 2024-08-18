using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Grid grid;
    [HideInInspector] public float cellWidth;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase occupiedTile;

    public void Awake()
    {
        cellWidth = transform.localScale.x * grid.cellSize.x;
    }

    public Vector3 CenterGridPosition(Vector3 sourcePos, Vector3 size)
    {
        //necessary becouse the resourceSource Awake() is somehow faster than the GridManager Awake()
        if(cellWidth == 0) cellWidth = transform.localScale.x * grid.cellSize.x;

        size.x %= 2;
        size.y %= 2;
        Vector3 gridPosition = cellWidth * (Vector3)grid.WorldToCell(sourcePos);
        gridPosition += size * (cellWidth / 2);
        if (size.x == 0 && (sourcePos.x - gridPosition.x >= cellWidth / 2)) gridPosition.x += cellWidth;
        if (size.y == 0 && (sourcePos.y - gridPosition.y >= cellWidth / 2)) gridPosition.y += cellWidth;
        gridPosition.z = 0;
        return gridPosition;
    }

    public Vector3 CenterGridPosition(Vector3 buildingSize)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return CenterGridPosition(mousePosition, buildingSize);
    }
    public void blockTiles(Vector3 centerPosition, Vector3 size)
    {
        centerPosition -= (size / 2) * cellWidth;
        Vector3Int offset = new(0, 0, 0);
        Vector3Int tilePos = tilemap.WorldToCell(centerPosition);
        for (int x = 0; x < size.x; x++)
        {
            offset.x = x;
            for(int y = 0; y < size.y; y++)
            {
                offset.y = y;
                tilemap.SetTile(tilePos + offset, occupiedTile);
            }
        }
    }
    public bool IsFree(Vector3 centerPosition, Vector3 size)
    {
        centerPosition -= (size / 2) * cellWidth;
        Vector3Int offset = new(0, 0, 0);
        Vector3Int tilePos = tilemap.WorldToCell(centerPosition);
        for (int x = 0; x < size.x; x++)
        {
            offset.x = x;
            for (int y = 0; y < size.y; y++)
            {
                offset.y = y;
                if (tilemap.GetTile(tilePos + offset) == occupiedTile) return false;
            }
        }
        return true;
    }
}
