using UnityEngine;

public class EquipmentSlot : MonoBehaviour, ISlotSystem
{
    [SerializeField]
    protected ESlotType slotType;
    public delegate void SetEquipItem(EquipedItemBase equipedItem);
    public SetEquipItem SetEquipDele;

    void Start()
    {
        GameObject CharacterObject = GameObject.FindGameObjectWithTag(ObjectTagString.CharacterObjectTagString);
        EquipedItem characterEquipItem = CharacterObject.GetComponent<CharacterBase>().GetCharacterEquipedItem();

        if(CharacterObject is null || characterEquipItem is null)
        {
            return;
        }

        SetEquipDele = characterEquipItem.SetEquipedItem;
    }

    public virtual bool EquiptItem(ItemBase InItem)
    {
        if(InItem is null)
        {
            return false;
        }

        EquipedItemBase equipedItemBase = null;
        if(InItem is EquipedItemBase)
        {
            equipedItemBase = InItem as EquipedItemBase;
        }

        if(equipedItemBase is null)
        {
            return false;
        }

        if (!ObjectValueTable.IsItemSlotTypeEqual(slotType, equipedItemBase.GetEquipmentType()))
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

        if(SetEquipDele.GetInvocationList().Length <= 0)
        {
            return false;
        }

        SetEquipDele?.Invoke(equipedItemBase);
        
        return true;
    }
}