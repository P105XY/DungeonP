using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public GameObject Item;
    public ItemBase itemBase;
    public string itemIndex;
    public EItemType itemtype;

    public void Start()
    {

    }

    public void InitItemData()
    {
        switch (itemtype)
        {
            case EItemType.COIN:
                CoinItemBase coinitem = new CoinItemBase();
                coinitem.InitItemDataFromDB(ref itemIndex);
                break;
            case EItemType.EQUIP:
                EquipedItemBase equipitem = new EquipedItemBase();
                equipitem.InitItemDataFromDB(ref itemIndex);
                break;
            case EItemType.USABLE:
                // UsableItemBase usableitem = new UsableItemBase();
                // usableitem.InitItemDataFromDB(ref itemIndex);
                break;
            default:
                break;
        }
    }

    public void Interation()
    {

    }

}
