using UnityEngine;

public class BuildMode : MonoBehaviour
{
    private float buildingTeam = 0;
    [SerializeField] private GridManager gridManager;
    [SerializeField] private BuildMenuManager buildMenuManager;
    private bool isBuilding = false;
    private BuildSlot buildSlot;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject ghostBuilding;
    private Vector3 mouseGridPosition;

    private Color redTrans  = new Color(1, .5f, .5f, .6f);
    private Color blueTrans = new Color(.5f, .7f, 1, .6f);

    public void Update()
    {
        if (isBuilding)
        {
            mouseGridPosition = gridManager.CenterGridPosition(buildSlot.buildingData.size);
            ghostBuilding.transform.position = mouseGridPosition;
            if (!gridManager.IsFree(mouseGridPosition, buildSlot.buildingData.size))
            {
                spriteRenderer.color = redTrans;
            }
            else
            {
                spriteRenderer.color = blueTrans;
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject building = Instantiate(buildSlot.buildingData.buildingPrefab, ghostBuilding.transform.position, Quaternion.identity);
                    building.transform.localScale = ghostBuilding.transform.localScale;
                    building.GetComponent<Entity>().entityStats["team"] = buildingTeam;
                    building.GetComponent<Building>().Setup(gridManager, mouseGridPosition, buildSlot.buildingData.size);
                    gridManager.blockTiles(mouseGridPosition, buildSlot.buildingData.size);
                    buildMenuManager.inventoryManager.RemoveBuildCost(buildSlot.buildingData.Costs);
                    buildMenuManager.UpdateSlotCost(buildSlot);
                    if(!Input.GetKey(KeyCode.LeftShift) || !buildSlot.costMet) stopBuilding();
                }
            }

            if (Input.GetMouseButtonDown(1)) stopBuilding();
        }
    }

    public void Build(BuildSlot _buildSlot)
    {
        buildMenuManager.menuManager.ChangeMenuState(MenuState.None);
        buildMenuManager.menuManager.lockMenu = true;

        buildSlot = _buildSlot;
        spriteRenderer.sprite = buildSlot.buildingData.buildingPrefab.GetComponent<SpriteRenderer>().sprite;
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
