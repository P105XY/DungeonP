public class EquipItem0003 : EquipedItemBase
{
    private string ItemIndex = "E_0003";

    // Start is called before the first frame update
   public override void Awake()
    {
        InitItemDataFromDB(ref ItemIndex);
        base.Awake();
    }

    
}
