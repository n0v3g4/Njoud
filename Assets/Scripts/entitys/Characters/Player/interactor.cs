using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactor : MonoBehaviour
{
    public Player player;
    private IInteractable interactable;
    // Update is called once per frame
    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(this.transform.position, player.entityStats["interactionRange"]);
        for (int i = 0; i < hits.Length; i++)
        {
            interactable = hits[i].GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactableFound(interactable);
            }
        }
    }
    private void interactableFound(IInteractable interactable)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            interactable.Interact(player);
        }
    }
}
