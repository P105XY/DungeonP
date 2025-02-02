using UnityEngine;

public struct FCharacterStatus
{
    public float BodyHealth; //Body Health, character die when this gage to zero 
    public float ArmHealth; //Arm Health, character can't every action when this gage to zero
    public float LegHealth; //Leg Health, character can't move when this gage to zero.
    public float BodyArmor; //substract to damage to body
    public float ArmArmor; //substract to damage to arm
    public float LegArmor; //substract to damage to leg
    public float CoinFlipAdvantage; //increase to coin flip success probablilty
    public float CoinFlipCount; //increase to coin flip Repeat count
    public float Luck; //increase to gain upper grade item 
    public float MoveSpeed; //movement speed
}

public class CharacterBase : MonoBehaviour
{
    private FCharacterStatus characterStatus;
    private EquipedItem CharacterEquiped;
    private CharacterBackpack CharacterBackpack;

    public Canvas InventoryCanvas;
    public Canvas EquipmentCanvas;

    public virtual void Awake()
    {
        CharacterEquiped = GetComponent<EquipedItem>();
    }

    public virtual void Start()
    {
        if(InventoryCanvas is null)
        {
            return;
        }

        if(EquipmentCanvas is null)
        {
            return;
        }
    }

    public virtual void Update()
    {
        MovementAction();
    }
    
    public virtual void FixedUpdate() 
    {
        
    }

    private void MovementAction()
    {

    }

    public FCharacterStatus GetCharacterStat()
    {
        return characterStatus;
    }

    public EquipedItem GetCharacterEquipedItem()
    {
        return CharacterEquiped;
    }
}
