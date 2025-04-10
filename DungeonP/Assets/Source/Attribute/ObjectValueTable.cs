using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum ESlotType
{
    NONE = 0,
    HEAD,
    BODY,
    TALISMAN,
    HAND,
    COIN
}

public enum EEquipmentType
{
    NONE = 0,
    HEAD,
    BODY,
    HAND,
    TALISMAN
}

//고정된 값을 사용할 때 쉽게 사용할 수 있도록 전역적인 변수 모음.
public static class ObjectValueTable
{
    public static readonly float InventorySlotSize = 29f;
    public static readonly float ItemXSize = 29.0f;
    public static readonly float ItemYSize = 28.2f;

    //item data base json file location
    public static readonly string EquipmentItemDBLocation = Path.Combine(Application.dataPath, "Database", "ItemDB", "EquipItemDB.json");
    public static readonly string CoinItemDBLocation = Path.Combine(Application.dataPath, "Database", "ItemDB", "CoinItemDB.json");
    public static readonly string UsableItemDBLocation = Path.Combine(Application.dataPath, "Database", "ItemDB", "UsableItemDB.json");
    public static readonly string BackpackItemDBLocation = Path.Combine(Application.dataPath, "Database", "ItemDB", "BackpackItemDB.json");

    public static readonly string LowriskEnemyDBLocation = Path.Combine(Application.dataPath, "Database","EnemyDB","LowRiskEnemyDB.json");

    public static readonly Dictionary<ESlotType, EEquipmentType> SlotToEquipmentMap = new()
{
    { ESlotType.NONE, EEquipmentType.NONE },
    { ESlotType.HEAD, EEquipmentType.HEAD },
    { ESlotType.BODY, EEquipmentType.BODY },
    { ESlotType.TALISMAN, EEquipmentType.TALISMAN },
    { ESlotType.HAND, EEquipmentType.HAND }
};

    public static readonly Dictionary<ESlotType, EItemType> SlotToItemTypeMap = new()
{
    {ESlotType.COIN, EItemType.COIN}
};

    public static bool IsItemSlotTypeEqual(ESlotType slotType, EEquipmentType EEquipmentType)
    {
        return SlotToEquipmentMap.TryGetValue(slotType, out EEquipmentType mapped) && mapped == EEquipmentType;
    }

    public static bool IsItemCoinSlotEqual(ESlotType slotType, EItemType eItemType)
    {
        return SlotToItemTypeMap.TryGetValue(slotType, out EItemType mapped) && mapped == eItemType;
    }
}