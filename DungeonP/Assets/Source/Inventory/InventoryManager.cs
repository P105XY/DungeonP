using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private GameObject[] EquipmentSlotList;
    private GameObject Inventory;

    void Start()
    {
        EquipmentSlotList = GameObject.FindGameObjectsWithTag(ObjectTagString.EquipmentSlot);
        Inventory = GameObject.FindGameObjectWithTag(ObjectTagString.InventoryCanvasTagString);
    }

    void Update()
    {
        
    }
}
