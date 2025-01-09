using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum EItemType
{
    NONE,
    EQUIP,
    BACKPACK,
    USABLE,
    COIN
}

public class ItemBase : MonoBehaviour
{
    public Sprite ItemSprite;
    public EItemType ECurrentItemType;
    public ItemSpace ItemUsingSpace;
    public int ItemID;
    //save each item data.

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
}
