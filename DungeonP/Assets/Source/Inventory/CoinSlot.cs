using UnityEngine;

class CoinSlot : MonoBehaviour, ISlotSystem
{
    public delegate void SetCoinEquip(CoinItemBase equipedCoin);
    public SetCoinEquip SetCoinEquipDele;
    private ESlotType slotType;

    void Start()
    {
        GameObject CharacterObject = GameObject.FindGameObjectWithTag(ObjectTagString.CharacterObjectTagString);
        EquipedItem characterEquipItem = CharacterObject.GetComponent<CharacterBase>().GetCharacterEquipedItem();

        if (CharacterObject is null || characterEquipItem is null)
        {
            return;
        }

        SetCoinEquipDele = characterEquipItem.SetEquipedCoin;

        slotType = ESlotType.COIN;
    }

    public bool EquiptItem(ItemBase InItem) 
    {
        if(InItem is null)
        {
            return false;
        }

        CoinItemBase coinItemBase = null;
        if(InItem is CoinItemBase)
        {
            coinItemBase = InItem as CoinItemBase;
        }

        if(coinItemBase is null)
        {
            return false;
        }

        if (!ObjectValueTable.IsItemCoinSlotEqual(slotType, coinItemBase.GetItemType()))
        {
            return false;
        }

        RectTransform itemRectTransform = null;
        if(!InItem.TryGetComponent<RectTransform>(out itemRectTransform))
        {
            return false;
        }

        InItem.transform.SetParent(transform);

        itemRectTransform.pivot = new Vector2(0.5f, 0.5f);
        itemRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        itemRectTransform.anchorMax = new Vector2(0.5f, 0.5f);

        itemRectTransform.anchoredPosition = new Vector2(0, 0);

        if (SetCoinEquipDele.GetInvocationList().Length <= 0)
        {
            return false;
        }

        SetCoinEquipDele?.Invoke(coinItemBase);
        
        return true;
    }
}