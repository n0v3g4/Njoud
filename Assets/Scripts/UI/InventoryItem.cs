using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TextMeshProUGUI countText;

    [HideInInspector] public itemData item;
    [HideInInspector] public int itemCount = 1;
    //store the Slot the Item returns to when dropped
    [HideInInspector] public Transform parentAfterDrag;

    public void InitialiseItem(itemData newItem, int count)
    {
        item = newItem;
        itemCount = count;
        image.sprite = newItem.image;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.SetText(itemCount.ToString());
        bool textActive = itemCount > 1;
        countText.gameObject.SetActive(textActive);
    }

    
    
   public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        //lift the item being dragged above all othen UI elements
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
