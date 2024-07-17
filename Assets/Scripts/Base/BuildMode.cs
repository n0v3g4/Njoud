using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    [SerializeField] private BuildMenuManager buildMenuManager;
    private bool isBuilding = false;
    private BuildingData buildingData;
    private GameObject ghostBuilding;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject ghostBuildingPrefab;
    private Vector3 mousePos;

    public void Awake()
    {
        ghostBuilding = Instantiate(ghostBuildingPrefab);
        spriteRenderer = ghostBuilding.GetComponent<SpriteRenderer>();
        ghostBuilding.SetActive(false);
    }

    public void Update()
    {
        if (isBuilding)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            ghostBuilding.transform.position = mousePos;
            if (Input.GetMouseButtonDown(0))
            {
                ghostBuilding.SetActive(false);
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
