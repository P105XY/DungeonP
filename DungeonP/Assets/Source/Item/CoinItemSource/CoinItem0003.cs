using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem0003 : CoinItemBase
{
    private string ItemIndex = "C_0003";
   public override void Awake()
    {
        InitItemDataFromDB(ref ItemIndex);
        base.Awake();
    }
}
