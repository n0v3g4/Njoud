using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BuildMenuManager buildMenuManager;
    private bool isBuilding = false;
    private BuildingData buildingData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject ghostBuilding;
    private Vector3 mouseGridPosition;

    private int buildingScalePersize = 3;

    public void Update()
    {
        if (isBuilding)
        {
            mouseGridPosition = gridManager.CenterGridPosition(buildingData.size);
            ghostBuilding.transform.position = mouseGridPosition;
            if (Input.GetMouseButtonDown(0))
            {
                if(gridManager.IsFree(mouseGridPosition - Vector3.Scale(gridManager.grid.cellSize / 2, buildingData.size), buildingData.size))
                {
                    GameObject building = Instantiate(buildingData.buildingPrefab, ghostBuilding.transform.position, Quaternion.identity);
                    building.transform.localScale = ghostBuilding.transform.localScale;
                    gridManager.blockTiles(mouseGridPosition, buildingData.size);
                    buildMenuManager.inventoryManager.RemoveBuildCost(buildingData.Costs);
                    buildMenuManager.UpdateSlotCost();
                    stopBuilding();
                }
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
        Vector3 localScale = new(Mathf.Max(buildingData.size.x, buildingData.size.y), Mathf.Max(buildingData.size.x, buildingData.size.y), 0);
        ghostBuilding.transform.localScale = localScale * buildingScalePersize;
        isBuilding = true;
        ghostBuilding.SetActive(true);
    }

    private void stopBuilding()
    {
        ghostBuilding.SetActive(false);
        isBuilding = false;
        buildMenuManager.menuManager.lockMenu = false;
    }
}
