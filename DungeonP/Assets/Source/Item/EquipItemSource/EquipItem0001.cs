public class EquipItem0001 : EquipedItemBase
{
    private string ItemIndex = "E_0001";

    // Start is called before the first frame update
    public override void Start()
    {
        InitItemDataFromDB(ref ItemIndex);
        base.Start();
    }

}
