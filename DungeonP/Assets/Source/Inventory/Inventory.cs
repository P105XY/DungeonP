using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting.Antlr3.Runtime;


public class Inventory : MonoBehaviour
{
    public byte InventoryXSize;
    public byte InventoryYSize;
    public List<List<InventorySlot>> ItemInventory = new List<List<InventorySlot>>();

    public Button InventoryItemButton;
    public Image InventoryBGImage;

    void Start()
    {
        ItemInventory.Clear();
        InitItemInventory();
        //clear item inventory first when game start.

    }

    private void InitItemInventory()
    {
        //init item invetory here.
        GameObject[] FindedInventorySlot = GameObject.FindGameObjectsWithTag(ObjectTagString.InventorySlotTagString);

        int ItemCountcolumn = 0;

        for (int i = 0; i < InventoryXSize; i++)
        {
            ItemInventory.Add(new List<InventorySlot>());
            for (int j = 0; j < InventoryYSize; j++)
            {
                InventorySlot CandidateInventorySlot;
                ItemCountcolumn = j;
                if (FindedInventorySlot[i + ItemCountcolumn].TryGetComponent<InventorySlot>(out CandidateInventorySlot))
                {
                    ItemInventory[i].Add(CandidateInventorySlot);
                }
                else
                {
                    return;
                }
            }
        }
    }

    public bool PlaceItem(byte x, byte y, ItemBase InItem)
    {
        if (!IsSlotAvaliable(x, y, InItem))
        {
            return false;
        }

        if (x < 0 || x > InventoryXSize || y < 0 || y > InventoryYSize)
        {
            return false;
        }

        if (x + InItem.GetItemXSpace() > InventoryXSize || y + InItem.GetItemYSpace() > InventoryYSize)
        {
            // 배열 범위를 초과하지 않도록 체크
            return false;
        }

        for (byte j = y; j < y + InItem.GetItemYSpace(); j++)
        {
            for (byte i = x; i < x + InItem.GetItemXSpace(); i++)
            {
                if (i >= InventoryXSize || j >= InventoryYSize)
                {
                    return false;
                }

                ItemInventory[i][j].SetItem(true);
            }
        }

        return true;
    }

    private bool IsSlotAvaliable(byte x, byte y, ItemBase InItem)
    {
        if (x < 0 || y < 0)
        {
            return false;
        }
        if (x >= InventoryXSize && y >= InventoryYSize)
        {
            return false;
        }

        for (byte slotX = 0; slotX < InventoryXSize; slotX++)
        {
            for (byte slotY = 0; slotY < InventoryYSize; slotY++)
            {
                if (slotX < 0 || slotX > InventoryXSize || slotY < 0 || slotY > InventoryYSize)
                {
                    return false;
                }
                if (this.ItemInventory[slotX][slotY] != null)
                {
                    return false;
                }
                if (this.ItemInventory[slotX + InItem.GetItemXSpace()][slotY + InItem.GetItemYSpace()] != null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public ItemBase GetClickedItem()
    {
        return null;
    }
}
