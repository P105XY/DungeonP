using System;
using System.IO;
using UnityEngine;

public class EquipedItemBase : ItemBase
{
    protected string itemName;
    protected float attackValue;
    protected float weightvalue;
    protected EEquipmentType equipmentType;

    public EquipedItemBase()
    {
        this.ECurrentItemType = EItemType.EQUIP;
    }

    public EEquipmentType GetEquipmentType()
    {
        return equipmentType;
    }

    protected void InitItemDataFromDB(ref string ItemIndex)
    {
        string jsonpath = ObjectValueTable.EquipmentItemDBLocation;
        if(!File.Exists(jsonpath))
        {
            Debug.Log("json file not found");
            return;
        }

        string FileData = File.ReadAllText(jsonpath);

        ItemCollection equipitemData = JsonUtility.FromJson<ItemCollection>(FileData);

        foreach(Item item in equipitemData.items)
        {
            if(!item.index.Equals(ItemIndex))
            {
                continue;
            }

            itemName = item.name;
            ItemSize = new ItemNameSpace.ItemSize(item.itemspace.X, item.itemspace.Y);
            attackValue = item.status.attack;
            weightvalue = item.status.weight;
            ItemName = itemName;
            equipmentType = (EEquipmentType)item.type;
        }
    }
}
