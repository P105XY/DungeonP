using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using ItemNameSpace;

using Vector2 = UnityEngine.Vector2;

public enum EItemType
{
    NONE,
    EQUIP,
    BACKPACK,
    USABLE,
    COIN
}

public class ItemBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [HideInInspector]
    public Sprite ItemSprite;
    [HideInInspector]
    public EItemType ECurrentItemType;
    [HideInInspector]
    public ItemSize ItemSize;
    [HideInInspector]
    public string ItemName;
    //save each item data.

    [HideInInspector]
    public List<InventorySlot> PlaceSlot;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Canvas parentCanvas;
    private Canvas equipCanvas;
    private Vector2 offset;
    private Inventory InventoryComponent;

    public int GetItemXSpace()
    {
        return ItemSize.GetItemXSpace();
    }

    public int GetItemYSpace()
    {
        return ItemSize.GetItemYSpace();
    }

    public EItemType GetItemType()
    {
        return ECurrentItemType;
    }

    public virtual void Awake()
    {
        if (!TryGetComponent<RectTransform>(out rectTransform))
        {
            return;
        }

        if (!TryGetComponent<CanvasGroup>(out canvasGroup))
        {
            return;
        }

        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);

        parentCanvas = GameObject.FindGameObjectWithTag(ObjectTagString.InventoryCanvasTagString).GetComponent<Canvas>();
        equipCanvas = GameObject.FindGameObjectWithTag(ObjectTagString.EquipmentCanvas).GetComponent<Canvas>();
        parentCanvas.TryGetComponent<Inventory>(out InventoryComponent);

        float itemXsizeOffset = ObjectValueTable.ItemXSize;
        float itemYsizeOffset = ObjectValueTable.ItemYSize;
        rectTransform.sizeDelta = new Vector2(GetItemXSpace() * itemXsizeOffset, GetItemYSpace() * itemYsizeOffset);
    }

    public void AddPlacementSlotData(InventorySlot placeSlot)
    {
        if(placeSlot is null)
        {
            return;
        }

        PlaceSlot.Add(placeSlot);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            foreach (InventorySlot slot in PlaceSlot)
            {
                if (slot is null)
                {
                    continue;
                }

                slot.ClearSlotData();
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {

        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            eventData.position,
            parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera,
            out Vector2 localMousePosition
        );

        offset = rectTransform.anchoredPosition - localMousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        List<RaycastResult> DropSlotRaycastResults = new List<RaycastResult>();
        GraphicRaycaster InventorySlotRaycaster;
        InventorySlot DropedInventorySlot;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            eventData.position,
            parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera,
            out Vector2 localPoint
        );

        parentCanvas.TryGetComponent<GraphicRaycaster>(out InventorySlotRaycaster);
        InventorySlotRaycaster.Raycast(eventData, DropSlotRaycastResults);
        foreach (RaycastResult result in DropSlotRaycastResults)
        {
            if (result.gameObject.CompareTag(ObjectTagString.InventoryItemPannel))
            {
                continue;
            }
            //ignore item panel, only find item slot tag object.

            result.gameObject.TryGetComponent<InventorySlot>(out DropedInventorySlot);
            if(DropedInventorySlot is null)
            {
                break;
            }

            InventoryComponent.ShowTemporaryItemPlacement(DropedInventorySlot, this);
            break;
        }

        rectTransform.anchoredPosition = localPoint + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        bool bIsPositionToSlot = RectTransformUtility.RectangleContainsScreenPoint
        (
            parentCanvas.transform as RectTransform,
            eventData.position,
            parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera
        );

        bool bIsDropOnSlot = false;
        List<RaycastResult> DropSlotRaycastResults = new List<RaycastResult>();
        List<RaycastResult> EquiptSlotRaycastResult = new List<RaycastResult>();
        GameObject DropItemSlotObject = null;
        GraphicRaycaster InventorySlotRaycaster;
        GraphicRaycaster EquipmentSlotRaycaster;
        InventorySlot DropedInventorySlot;
        EquipmentSlot DropedEquipmentSlot;

        if (!parentCanvas.TryGetComponent<GraphicRaycaster>(out InventorySlotRaycaster))
        {
            return;
        }
        
        if(!equipCanvas.TryGetComponent<GraphicRaycaster>(out EquipmentSlotRaycaster))
        {
            return;
        }

        InventorySlotRaycaster.Raycast(eventData, DropSlotRaycastResults);
        EquipmentSlotRaycaster.Raycast(eventData, EquiptSlotRaycastResult);
        if (DropSlotRaycastResults.Count > 0 || EquiptSlotRaycastResult.Count > 0)
        {
            bIsDropOnSlot = true;
        }

        foreach (RaycastResult result in DropSlotRaycastResults)
        {
            if (!result.gameObject.CompareTag(ObjectTagString.InventorySlotTagString))
            {
                continue;
            }

            DropItemSlotObject = result.gameObject;
        }

        foreach(RaycastResult result in EquiptSlotRaycastResult)
        {
            if(!result.gameObject.CompareTag(ObjectTagString.EquipmentSlot))
            {
                continue;
            }

            DropItemSlotObject = result.gameObject;
        }

        if (DropItemSlotObject.TryGetComponent<InventorySlot>(out DropedInventorySlot) && DropItemSlotObject != null)
        {
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.pivot = new Vector2(0, 1);

            transform.SetParent(parentCanvas.transform.GetChild(1));

            // 슬롯 오브젝트의 컴포넌트를 찾지 못할경우 원래 위치로 복귀.
            if (DropItemSlotObject == null ||
                !InventoryComponent.PlaceItemToSlot(DropedInventorySlot, this) ||
            (!bIsPositionToSlot && !bIsDropOnSlot) || DropItemSlotObject == null)
            {
                rectTransform.anchoredPosition = originalPosition;
                canvasGroup.alpha = 1f;
                canvasGroup.blocksRaycasts = true;
                InventoryComponent.ClearTemporaryShowSlot();
                return;
            }

            RectTransform DropRectTrasnform;
            if (!DropItemSlotObject.TryGetComponent<RectTransform>(out DropRectTrasnform))
            {
                return;
            }

            Vector2 itemdropPos = DropRectTrasnform.anchoredPosition;

            rectTransform.anchoredPosition = itemdropPos;
        }
        else if (DropItemSlotObject.TryGetComponent<EquipmentSlot>(out DropedEquipmentSlot) && DropItemSlotObject != null && !DropedEquipmentSlot.EquiptItem(this))
        {
            rectTransform.anchoredPosition = originalPosition;
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            InventoryComponent.ClearTemporaryShowSlot();
            return;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        InventoryComponent.ClearTemporaryShowSlot();
    }

    //operator functions
    public static bool operator ==(ItemBase CandidateItem, ItemBase CompareItem)
    {
        if (CandidateItem is null || CompareItem is null)
        {
            return false;
        }

        string candidateName = CandidateItem.ItemName;
        string compareName = CompareItem.ItemName;

        return candidateName == compareName;
    }

    public static bool operator !=(ItemBase CandidateItem, ItemBase CompareItem)
    {
        if (CandidateItem is null || CompareItem is null)
        {
            return false;
        }

        string candidateName = CandidateItem.ItemName;
        string compareName = CompareItem.ItemName;

        return candidateName == compareName;
    }

    public override bool Equals(object obj)
    {
        if (obj is ItemBase other)
        {
            return ItemName == other.ItemName;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return ItemName.GetHashCode();
    }

}
