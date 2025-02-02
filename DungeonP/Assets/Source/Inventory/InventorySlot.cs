using UnityEngine;

public struct SlotPositionStruct
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public SlotPositionStruct(int Inx, int Iny)
    {
        X = Inx;
        Y = Iny;
    }
}

public class InventorySlot : MonoBehaviour
{
    private RectTransform slotRectTrasnform;
    public bool IsItemSlotAvaliable;
    public SlotPositionStruct SlotPosition { get; private set; }
    public ItemBase SlotItem {get; private set;}

    public void Start()
    {
        if (!TryGetComponent<RectTransform>(out slotRectTrasnform))
        {
            return;
        }

        slotRectTrasnform.pivot = new Vector2(0, 1);
        IsItemSlotAvaliable = true;
        SlotItem = null;
        SlotPosition = new SlotPositionStruct(0, 0);
    }

    public void InitSlot(int x, int y)
    {
        SlotPosition = new SlotPositionStruct(x, y);
    }

    public SlotPositionStruct GetSlot()
    {
        return new SlotPositionStruct(SlotPosition.Y, SlotPosition.X);
    }

    public void FillItemSlot()
    {
        IsItemSlotAvaliable = false;
    }

    public void FillSlotItemData(ItemBase InItem)
    {
        if(InItem is null)
        {
            return;
        }

        SlotItem = InItem;
    }

    public bool IsSlotAvaliable()
    {
        return IsItemSlotAvaliable && SlotItem is null;
    }

    public ItemBase GetInSlotItem()
    {
        if(SlotItem is null)
        {
            return null;
        }

        return SlotItem;
    }

    public void ClearSlotData()
    {
        IsItemSlotAvaliable = true;
        SlotItem = null;
    }

    //operator functions
    public static bool operator ==(InventorySlot CandidateItem, InventorySlot CompareItem)
    {
        if (CandidateItem is null || CompareItem is null)
        {
            return false;
        }

        SlotPositionStruct candidateSlotPos = CandidateItem.SlotPosition;
        SlotPositionStruct compareSlotPos = CompareItem.SlotPosition;

        return candidateSlotPos.Equals(compareSlotPos);
    }

    public static bool operator !=(InventorySlot CandidateItem, InventorySlot CompareItem)
    {
        if (CandidateItem is null || CompareItem is null)
        {
            return false;
        }

        SlotPositionStruct candidateSlotPos = CandidateItem.SlotPosition;
        SlotPositionStruct compareSlotPos = CompareItem.SlotPosition;

        return candidateSlotPos.Equals(compareSlotPos);
    }

    public override bool Equals(object obj)
    {
        if (obj is InventorySlot other)
        {
            return (SlotPosition.X == other.SlotPosition.Y) && (SlotPosition.X == other.SlotPosition.Y);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return SlotPosition.GetHashCode();
    }
}
