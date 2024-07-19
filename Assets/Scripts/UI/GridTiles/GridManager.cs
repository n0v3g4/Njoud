using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase occupiedTile;

    public Vector3 CenterGridPosition(Vector3 sourcePos, Vector3 size)
    {
        size.x %= 2;
        size.y %= 2;
        Vector3 gridPosition = grid.WorldToCell(sourcePos);
        gridPosition += Vector3.Scale(size, grid.cellSize / 2);
        if (size.x == 0 && (sourcePos.x - gridPosition.x >= grid.cellSize.x / 2)) gridPosition.x += grid.cellSize.x;
        if (size.y == 0 && (sourcePos.y - gridPosition.y >= grid.cellSize.y / 2)) gridPosition.y += grid.cellSize.y;
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
        centerPosition -= Vector3.Scale(grid.cellSize / 2, size);
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
    public bool IsFree(Vector3 position, Vector3 size)
    {
        Vector3Int offset = new(0, 0, 0);
        Vector3Int tilePos = tilemap.WorldToCell(position);
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
