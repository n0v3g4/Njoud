using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TextMeshProUGUI countText;

    private InventoryManager inventoryManager;

    [HideInInspector] public itemData item;
    [HideInInspector] public int count;
    //store the Slot the Item returns to when dropped
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public bool originBlocked = false;

    public void InitialiseItem(itemData newItem, int _count, InventoryManager _inventoryManager)
    {
        item = newItem;
        count = _count;
        inventoryManager = _inventoryManager;
        image.sprite = newItem.image;
        countText.raycastTarget = false;
        RefreshCount();
    }

    public void RefreshCount()
    {
        countText.SetText(count.ToString());
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }

   public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        //lift the item being dragged above all othen UI elements
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        if (Input.GetKey(KeyCode.LeftShift) && count>1)
        {
            //split the stack
            int split = Mathf.FloorToInt(count / 2);
            int remainder = count - split;
            count = split;
            RefreshCount();
            //create a new InventoryItem for the remainder
            inventoryManager.SpawnNewItem(item, parentAfterDrag.GetComponent<InventorySlot>(), remainder);
            originBlocked = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (parentAfterDrag.childCount != 0)
        {
            //this happens only when splitting a Stack and not depositing the split fully
            InventoryItem occupyingItem = parentAfterDrag.GetChild(0).gameObject.GetComponent<InventoryItem>();
            occupyingItem.count += count;
            occupyingItem.RefreshCount();
            Destroy(gameObject);
        }
        else transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        originBlocked = false;
    }
}
