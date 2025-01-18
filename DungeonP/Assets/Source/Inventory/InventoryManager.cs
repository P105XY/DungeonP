using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Inventory CharacterInventory;
    public GameObject SlotPrefab;
    public GameObject BGPrefab;
    public Transform InventorySlotPannel;
    public int InventorySize = 100;

    private int screenXposition;
    private int screenYPosition;
    private CanvasScaler inventoryCanvasScaler;

    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<CanvasScaler>(out inventoryCanvasScaler))
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
