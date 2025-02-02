public class EquipItem0002 : EquipedItemBase
{
    private string ItemIndex = "E_0002";

    // Start is called before the first frame update
   public override void Awake()
    {
        InitItemDataFromDB(ref ItemIndex);
        base.Awake();
    }

    
}
