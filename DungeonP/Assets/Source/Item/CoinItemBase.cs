using System.IO;
using UnityEngine;

public class CoinItemBase : ItemBase
{
    protected string itemName;
    protected float luck;


    public CoinItemBase()
    {
        ECurrentItemType = EItemType.COIN;
    }

    public void InitItemDataFromDB(ref string ItemIndex)
    {
        string jsonpath = ObjectValueTable.CoinItemDBLocation;
        if (!File.Exists(jsonpath))
        {
            Debug.Log("json file not found");
            return;
        }

        string FileData = File.ReadAllText(jsonpath);

        CoinItemCollection equipitemData = JsonUtility.FromJson<CoinItemCollection>(FileData);

        foreach (CoinItem item in equipitemData.items)
        {
            if (!item.index.Equals(ItemIndex))
            {
                continue;
            }

            itemName = item.name;
            ItemSize = new ItemNameSpace.ItemSize(item.itemspace.X, item.itemspace.Y);
            luck = item.coinstatus.luck;
            ItemName = itemName;
        }
    }
}
