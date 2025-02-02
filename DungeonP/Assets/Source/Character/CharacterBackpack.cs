using System.Collections.Generic;
using UnityEngine;

public class CharacterBackpack : MonoBehaviour
{
    public List<ItemBase> BackpackItems;

    private Inventory InventoryComponent;

    void Start()
    {
        GameObject InventoryCanvas = GameObject.FindGameObjectWithTag(ObjectTagString.InventoryCanvasTagString);

        if(!InventoryCanvas.TryGetComponent<Inventory>(out InventoryComponent))
        {
            return;
        }
    }

    public void GetItem(ItemBase GetItem)
    {
        if(GetItem == null)
        {
            return;
        }

        BackpackItems.Add(GetItem);
    }

    public void RemoveItem(ItemBase RemoveItem)
    {
        if(RemoveItem == null)
        {
            return;
        }

        BackpackItems.Remove(RemoveItem);

    }

}
