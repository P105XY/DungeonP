public class EquipItem0003 : EquipedItemBase
{
    private string ItemIndex = "E_0003";

    // Start is called before the first frame update
    public override void Start()
    {
        InitItemDataFromDB(ref ItemIndex);
        base.Start();
    }

    
}
