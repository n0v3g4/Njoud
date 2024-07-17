using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform inventoryHolder;
    public Transform buildMenuHolder;
    [HideInInspector] public bool lockMenu = false;

    [SerializeField] private BuildMenuManager buildMenuManager;

    public Dictionary<MenuState, Transform> stateTransforms = new Dictionary<MenuState, Transform>();
    private MenuState currentState = MenuState.None;

    void Start()
    {
        stateTransforms[MenuState.None] = null;
        stateTransforms[MenuState.Inventory] = inventoryHolder;
        stateTransforms[MenuState.BuildMenu] = buildMenuHolder;
    }

    void Update()
    {
        if (Input.inputString != null && !lockMenu)
        {
            if (Input.GetKeyDown(KeyCode.Q)) ChangeMenuState(MenuState.BuildMenu);
            if (Input.GetKeyDown(KeyCode.E)) ChangeMenuState(MenuState.Inventory);
        }
    }

    public void ChangeMenuState(MenuState targetState)
    {
        if (currentState == targetState)
        {
            toggleMenu(stateTransforms[targetState]);
            currentState = MenuState.None;
            return;
        }
        toggleMenu(stateTransforms[currentState]);
        toggleMenu(stateTransforms[targetState]);
        currentState = targetState;

        //updates when spesific states are targettet
        if (targetState == MenuState.BuildMenu) buildMenuManager.UpdateSlotCost();
    }

    private void toggleMenu(Transform menu)
    {
        if (!menu) return;
        if (menu.gameObject.activeSelf) menu.gameObject.SetActive(false);
        else menu.gameObject.SetActive(true);
    }
}

public enum MenuState
{
    None,
    BuildMenu,
    Inventory
}
