using UnityEngine;
using UnityEngine.EventSystems;
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

public class ItemBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Sprite ItemSprite;
    public EItemType ECurrentItemType;
    public ItemSpace ItemUsingSpace;
    public int ItemID;
    //save each item data.

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Canvas parentCanvas;

    private float imageSpriteSize;
    private Vector2 ImageSize;
    private Vector2 offset;
    private Vector2 DropPosition;

    public byte GetItemXSpace()
    {
        return ItemUsingSpace.GetItemXSpace();
    }

    public byte GetItemYSpace()
    {
        return ItemUsingSpace.GetItemYSpace();
    }

    public EItemType GetItemType()
    {
        return ECurrentItemType;
    }

    public void Start()
    {
        if(!TryGetComponent<RectTransform>(out rectTransform))
        {
            return;
        }

        if(!TryGetComponent<CanvasGroup>(out canvasGroup))
        {
            return;
        }

        parentCanvas = GameObject.FindGameObjectWithTag(ObjectTagString.InventoryCanvasTagString).GetComponent<Canvas>();

        // imageSpriteSize = ObjectValueTable.InventorySlotSize;
        // ImageSize.x = imageSpriteSize * ItemUsingSpace.GetItemXSpace();
        // ImageSize.y = imageSpriteSize * ItemUsingSpace.GetItemYSpace();

        // rectTransform.sizeDelta = new Vector2(ImageSize.x, ImageSize.y);
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
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform,
            eventData.position,
            parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera,
            out Vector2 localPoint
        );
        rectTransform.anchoredPosition = localPoint + offset;
        DropPosition = localPoint + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        string slotname = eventData.pointerDrag.name;
        Debug.Log("slot name : " + slotname);

        // 슬롯으로 드롭되지 않았을 경우 원래 위치로 복귀
        if (!RectTransformUtility.RectangleContainsScreenPoint(
            parentCanvas.transform as RectTransform,
            eventData.position,
            parentCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : parentCanvas.worldCamera))
        {
            rectTransform.anchoredPosition = originalPosition;
        }
        else
        {
            rectTransform.anchoredPosition = DropPosition;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
