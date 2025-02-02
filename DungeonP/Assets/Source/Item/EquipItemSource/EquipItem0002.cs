public class EquipItem0002 : EquipedItemBase
{
    private string ItemIndex = "E_0002";

    // Start is called before the first frame update
    public override void Start()
    {
        InitItemDataFromDB(ref ItemIndex);
        base.Start();
    }

    
}
