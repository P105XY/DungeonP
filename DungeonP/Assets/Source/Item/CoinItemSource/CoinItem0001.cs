using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem0001 : CoinItemBase
{
    private string ItemIndex = "C_0001";
    public override void Start()
    {
        InitItemDataFromDB(ref ItemIndex);
        base.Start();
    }
}
