using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FCharacterStatus
{
    public float BodyHealth; //Body Health, character die when this gage to zero 
    public float ArmHealth; //Arm Health, character can't every action when this gage to zero
    public float LegHealth; //Leg Health, character can't move when this gage to zero.
    public float BodyArmor; //substract to damage to body
    public float ArmArmor; //substract to damage to arm
    public float LegArmor; //substract to damage to leg
    public byte CoinFlipAdvantage; //increase to coin flip success probablilty
    public byte CoinFlipCount; //increase to coin flip Repeat count
    public byte Luck; //increase to gain upper grade item 
}

public class CharacterBase : MonoBehaviour
{
    private FCharacterStatus characteStatus;
    private Inventory CharacterInventory;
    private EquipedItem CharacterEquiped;

    


}
