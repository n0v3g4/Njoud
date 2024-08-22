using UnityEngine;

public class BuildMode : MonoBehaviour
{
    private float buildingTeam = 0;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BuildMenuManager buildMenuManager;
    private bool isBuilding = false;
    private BuildingData buildingData;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject ghostBuilding;
    private Vector3 mouseGridPosition;

    private Color redTrans  = new Color(1, .5f, .5f, .6f);
    private Color blueTrans = new Color(.5f, .7f, 1, .6f);

    public void Update()
    {
        if (isBuilding)
        {
            mouseGridPosition = gridManager.CenterGridPosition(buildingData.size);
            ghostBuilding.transform.position = mouseGridPosition;
            if (!gridManager.IsFree(mouseGridPosition, buildingData.size))
            {
                spriteRenderer.color = redTrans;
            }
            else
            {
                spriteRenderer.color = blueTrans;
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject building = Instantiate(buildingData.buildingPrefab, ghostBuilding.transform.position, Quaternion.identity);
                    building.transform.localScale = ghostBuilding.transform.localScale;
                    building.GetComponent<Entity>().entityStats["team"] = buildingTeam;
                    building.GetComponent<Building>().Setup(gridManager, mouseGridPosition, buildingData.size);
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
