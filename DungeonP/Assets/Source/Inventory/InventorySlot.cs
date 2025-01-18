using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    private bool isItemAvaliable;

    public void Start()
    {
        isItemAvaliable = false;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop Item Slot");
        ItemBase draggableItem = eventData.pointerDrag.GetComponent<ItemBase>();
        if(draggableItem == null)
        {
            return;
        }

        if(GetCurrentItem())
        {
            return;
        }

        SetItem(isItemAvaliable);

    }

    public void SetItem(bool inIsItemAvaliable)
    {
        this.isItemAvaliable = inIsItemAvaliable;
    }

    private bool GetCurrentItem()
    {
        return isItemAvaliable;
    }
}
