using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private BuildMenuManager buildMenuManager;
    private bool isBuilding = false;
    private BuildingData buildingData;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject ghostBuilding;
    private Vector3 mouseGridPosition;

    private int buildingScalePersize = 3;

    public void Awake()
    {
        spriteRenderer = ghostBuilding.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if (isBuilding)
        {
            mouseGridPosition = centerGridPosition(buildingData.size);
            ghostBuilding.transform.position = mouseGridPosition;
            if (Input.GetMouseButtonDown(0))
            {
                GameObject building = Instantiate(buildingData.buildingPrefab, ghostBuilding.transform.position, Quaternion.identity);
                buildMenuManager.inventoryManager.RemoveBuildCost(buildingData.Costs);
                buildMenuManager.UpdateSlotCost();
                stopBuilding();
            }
            if (Input.GetMouseButtonDown(1)) stopBuilding();
        }
    }

    public void Build(BuildingData _buildingData)
    {
        buildMenuManager.menuManager.ChangeMenuState(MenuState.None);
        buildMenuManager.menuManager.lockMenu = true;

        buildingData = _buildingData;
        spriteRenderer.sprite = buildingData.buildingPrefab.GetComponent<SpriteRenderer>().sprite;
        ghostBuilding.transform.localScale = buildingData.size * buildingScalePersize;
        isBuilding = true;
        ghostBuilding.SetActive(true);
    }

    private void stopBuilding()
    {
        ghostBuilding.SetActive(false);
        isBuilding = false;
        buildMenuManager.menuManager.lockMenu = false;
    }

    private Vector3 centerGridPosition(Vector3 buildingSize)
    {
        buildingSize.x %= 2;
        buildingSize.y %= 2;
        Vector3 gridPosition = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        gridPosition += Vector3.Scale(buildingSize, grid.cellSize / 2);
        gridPosition.z = 0;

        return gridPosition;
    }
}
