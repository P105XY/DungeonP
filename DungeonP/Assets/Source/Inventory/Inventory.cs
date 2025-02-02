using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int InventoryXSize;
    public int InventoryYSize;
    public List<List<InventorySlot>> InventorySlotList = new List<List<InventorySlot>>();
    public List<ItemBase> InventoryItemList = new List<ItemBase>();
    public Button InventoryItemButton;
    public Image InventoryBGImage;

    private Color SlotAvaliableColor;
    private Color SlotNotAvaliableColor;
    private Color SlotPlacementableColor;
    private Color SlotUnplacementableColor;

    void Start()
    {
        InventorySlotList.Clear();
        InitItemInventory();

        UnityEngine.ColorUtility.TryParseHtmlString("#FFFFFF59", out SlotAvaliableColor);
        UnityEngine.ColorUtility.TryParseHtmlString("#00000059", out SlotNotAvaliableColor);
        UnityEngine.ColorUtility.TryParseHtmlString("#7CE59559", out SlotPlacementableColor);
        UnityEngine.ColorUtility.TryParseHtmlString("#C5394D59", out SlotUnplacementableColor);
    }

    private void InitItemInventory()
    {
        //init item invetory here.
        GameObject[] FindedInventorySlot = GameObject.FindGameObjectsWithTag(ObjectTagString.InventorySlotTagString);
        System.Array.Reverse(FindedInventorySlot);

        InventorySlotList = new List<List<InventorySlot>>(10);

        int ItemCountcolumn = 0;
        for (int i = 0; i < InventoryYSize; i++)
        {
            InventorySlotList.Add(new List<InventorySlot>());
            for (int j = 0; j < InventoryXSize; j++)
            {
                InventorySlot CandidateInventorySlot;
                ItemCountcolumn = j;
                if (FindedInventorySlot[(i * 10) + ItemCountcolumn].TryGetComponent<InventorySlot>(out CandidateInventorySlot))
                {
                    CandidateInventorySlot.InitSlot(i, j);
                    InventorySlotList[i].Add(CandidateInventorySlot);
                }
            }
        }
    }

    public bool PlaceItem(int x, int y, ItemBase InItem)
    {
        if (!IsSlotAvaliable(x, y, InItem))
        {
            return false;
        }

        int XMaxSize = InItem.GetItemXSpace();
        int YMaxSize = InItem.GetItemYSpace();

        for (int j = y; j < y + YMaxSize; j++)
        {
            for (int i = x; i < x + XMaxSize; i++)
            {
                InventorySlotList[j][i].FillItemSlot();
                InventorySlotList[j][i].FillSlotItemData(InItem);
                InItem.AddPlacementSlotData(InventorySlotList[j][i]);
            }
        }

        return true;
    }

    public bool PlaceItemToSlot(in InventorySlot DropedSlot, in ItemBase InItem)
    {
        if (DropedSlot == null)
        {
            return false;
        }

        if (InItem == null)
        {
            return false;
        }

        SlotPositionStruct Slotpos = new SlotPositionStruct(DropedSlot.GetSlot().X, DropedSlot.GetSlot().Y);
        if (!PlaceItem(Slotpos.X, Slotpos.Y, InItem))
        {
            return false;
        }

        return true;
    }

    private bool IsSlotAvaliable(int x, int y, ItemBase InItem)
    {
        if (x < 0 || y < 0)
        {
            return false;
        }

        if (x >= InventoryXSize || y >= InventoryYSize)
        {
            return false;
        }

        int XMaxSize = InItem.GetItemXSpace();
        int YMaxSize = InItem.GetItemYSpace();

        for (int slotY = y; slotY < y + YMaxSize; slotY++)
        {
            for (int slotX = x; slotX < x + XMaxSize; slotX++)
            {
                if (slotX < 0 || slotX >= InventoryXSize || slotY < 0 || slotY >= InventoryYSize)
                {
                    return false;
                }
                if (!InventorySlotList[slotY][slotX].IsSlotAvaliable())
                {
                    return false;
                }
            }
        }

        return true;
    }

    public bool InsertItemInventory(in ItemBase InItem)
    {
        //insert item to nearest first item slot.
        if (InItem == null)
        {
            return false;
        }

        int InsertSlotX = -1;
        int InsertSlotY = -1;

        for (int slotY = 0; slotY < InventoryYSize; slotY++)
        {
            for (int slotX = 0; slotX < InventoryXSize; slotX++)
            {
                if (slotX < 0 || slotX >= InventoryXSize || slotY < 0 || slotY >= InventoryYSize)
                {
                    return false;
                }
                
                if(!InventorySlotList[slotY][slotX].IsSlotAvaliable())
                {
                    continue;
                }

                InsertSlotX = slotX;
            }
            InsertSlotY = slotY;
        }

        PlaceItem(InsertSlotX, InsertSlotY, InItem);
        return true;
    }

    public void RemoveInventorySlotData(InventorySlot ClearStartSlot, ItemBase InItem)
    {
        if (ClearStartSlot is null || InItem is null)
        {
            return;
        }

        SlotPositionStruct Slotpos = ClearStartSlot.SlotPosition;

        int x = Slotpos.X;
        int y = Slotpos.Y;
        int itemX = InItem.GetItemXSpace();
        int itemY = InItem.GetItemYSpace();

        for (int j = y; j < y + itemY; j++)
        {
            for (int i = x; i < x + itemX; i++)
            {
                if (i >= InventoryXSize || j >= InventoryYSize)
                {
                    continue;
                }

                InventorySlotList[j][i].ClearSlotData();
            }
        }
    }

    public void ShowTemporaryItemPlacement(in InventorySlot DropedSlot, in ItemBase InItem)
    {
        if (DropedSlot == null)
        {
            return;
        }

        if (InItem == null)
        {
            return;
        }

        SlotPositionStruct Slotpos = new SlotPositionStruct(DropedSlot.GetSlot().X, DropedSlot.GetSlot().Y);
        if (Slotpos.X < 0 || Slotpos.X >= InventoryXSize || Slotpos.Y < 0 || Slotpos.Y >= InventoryYSize)
        {
            return;
        }

        ShowSlotPlacementable(IsSlotAvaliable(Slotpos.X, Slotpos.Y, InItem), Slotpos.X, Slotpos.Y, InItem);
    }

    private void ShowSlotPlacementable(bool bIsAvaliable, int x, int y, in ItemBase InItem)
    {
        for (int j = 0; j < InventoryYSize; j++)
        {
            for (int i = 0; i < InventoryXSize; i++)
            {
                if (i < 0 || i >= InventoryXSize || j < 0 || j >= InventoryYSize)
                {
                    continue;
                }

                Image slotImage;
                InventorySlot CandidateSlot;

                if (!InventorySlotList[j][i].TryGetComponent<InventorySlot>(out CandidateSlot))
                {
                    continue;
                }

                if (!CandidateSlot.TryGetComponent<Image>(out slotImage))
                {
                    continue;
                }

                if (CandidateSlot.IsSlotAvaliable())
                {
                    slotImage.color = SlotAvaliableColor;
                }
                else
                {
                    slotImage.color = SlotNotAvaliableColor;
                }
            }
        }

        for (int j = y; j < y + InItem.GetItemYSpace(); j++)
        {
            for (int i = x; i < x + InItem.GetItemXSpace(); i++)
            {
                if (i < 0 || i >= InventoryXSize || j < 0 || j >= InventoryYSize)
                {
                    continue;
                }

                Image slotImage;
                InventorySlot CandidateSlot;

                if (!InventorySlotList[j][i].TryGetComponent<InventorySlot>(out CandidateSlot))
                {
                    continue;
                }

                if (!CandidateSlot.TryGetComponent<Image>(out slotImage))
                {
                    continue;
                }

                if (bIsAvaliable)
                {
                    slotImage.color = SlotPlacementableColor;
                }
                else
                {
                    slotImage.color = SlotUnplacementableColor;
                }
            }
        }
    }

    public void ClearTemporaryShowSlot()
    {
        for (int j = 0; j < InventoryYSize; j++)
        {
            for (int i = 0; i < InventoryXSize; i++)
            {
                Image slotImage;
                InventorySlot CandidateSlot;

                if (!InventorySlotList[j][i].TryGetComponent<InventorySlot>(out CandidateSlot))
                {
                    continue;
                }

                if (!CandidateSlot.TryGetComponent<Image>(out slotImage))
                {
                    continue;
                }

                if (CandidateSlot.IsSlotAvaliable())
                {
                    slotImage.color = SlotAvaliableColor;
                }
                else
                {
                    slotImage.color = SlotNotAvaliableColor;
                }
            }
        }
    }

    public InventorySlot SearchItemStartPos(in InventorySlot inInventorySlot, in ItemBase inItem)
    {
        if (inInventorySlot == null || inItem == null)
        {
            return null;
        }
        SlotPositionStruct Slotpos = inInventorySlot.SlotPosition;

        int x = Slotpos.X;
        int y = Slotpos.Y;
        int itemX = inItem.GetItemXSpace();
        int itemY = inItem.GetItemYSpace();

        int compareDim = math.max(itemX, itemY);
        InventorySlot resultSlot = null;

        for (int j = y; j >= 0; j--)
        {
            for (int i = x; i >= 0; i--)
            {
                if (i >= InventoryXSize || j >= InventoryYSize)
                {
                    continue;
                }

                InventorySlot CandidateSlot = InventorySlotList[j][i];
                ItemBase CandidateItem = CandidateSlot.GetInSlotItem();
                if (CandidateSlot is null || CandidateItem is null)
                {
                    continue;
                }

                if (CandidateItem == inItem)
                {
                    resultSlot = CandidateSlot;
                }
            }
        }

        return resultSlot;
    }
}
