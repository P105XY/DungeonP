using UnityEngine;

//현재 캐릭터가 착용하고 있는 아이템을 정리한 클래스.
//가방이 아닌 착용중인 아이템을 관리한다.
public class EquipedItem : MonoBehaviour
{
    private EquipedItemBase headEquipItem;
    private EquipedItemBase bodyEquipItem;
    private EquipedItemBase handEquipItem;
    private EquipedItemBase talismanEquipItem;

    private CoinItemBase coinEquipItem;

    void InitEquipedItemList()
    {
        
    }

    public void SetEquipedItem(EquipedItemBase EquipItem)
    {
        if(EquipItem is null)
        {
            return;
        }

        EEquipmentType EEquipmentType = EquipItem.GetEquipmentType();

        switch (EEquipmentType)
        {
            case EEquipmentType.HEAD:
                headEquipItem = EquipItem;
                break;

            case EEquipmentType.BODY:
                bodyEquipItem = EquipItem;
                break;

            case EEquipmentType.HAND:
                handEquipItem = EquipItem;
                break;

            case EEquipmentType.TALISMAN:
                talismanEquipItem = EquipItem;
                break;

            default:
                break;
        }
    }

    public void SetEquipedCoin(CoinItemBase coinItem)
    {
        if(coinItem is null)
        {
            return;
        }

        coinEquipItem = coinItem;
    }
}
