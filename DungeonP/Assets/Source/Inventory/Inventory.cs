using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Inventory : MonoBehaviour
{
    public byte InventoryXSize;
    public byte InventoryYSize;
    public List<List<ItemBase>> ItemInventory = new List<List<ItemBase>>();

    public Button InventoryItemButton;
    public Image InventoryBGImage;

    private List<Button> InventoryButtonList = new List<Button>();
    private List<Image> InventoryBGImageList = new List<Image>();

    private GridLayoutGroup InventoryBackGroundPannel;
    private GridLayoutGroup InventoryItemSlotPannel;
    private GameObject InventoryCanvas;

    void Start()
    {
        InventoryCanvas = GameObject.FindWithTag(ObjectTagString.InventoryCanvasTagString);

        if (InventoryCanvas == null)
        {
            return;
        }

        Debug.Log("Get canvas");

        Transform BGPannel = InventoryCanvas.transform.GetChild(0);
        Transform SlotPannel = InventoryCanvas.transform.GetChild(1);

        if (BGPannel == null)
        {
            return;
        }

        if (SlotPannel == null)
        {
            return;
        }

        InventoryBackGroundPannel = BGPannel.GetComponent<GridLayoutGroup>();
        InventoryItemSlotPannel = SlotPannel.GetComponent<GridLayoutGroup>();

        if (InventoryBackGroundPannel == null)
        {
            return;
        }
        if (InventoryItemSlotPannel == null)
        {
            return;
        }
        //init Inventory Grid pannel.

        ItemInventory.Clear();
        InitItemInventory();
        //clear item inventory first when game start.

    }

    private void InitItemInventory()
    {

        //init item invetory here.
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

                ItemInventory[i][j] = InItem;
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
