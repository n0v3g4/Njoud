using UnityEngine;

public class craftignTable : MonoBehaviour, IInteractable
{
    public void Interact(Player player)
    {
        player.menuManager.ChangeMenuState(MenuState.CraftMenu);
    }
}
