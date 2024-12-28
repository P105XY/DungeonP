using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemType
{
    NONE,
    EQUIP,
    USABLE,
    COIN
}

public class ItemBase
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
}
