using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public struct FCharacterStatus
{
    public float BodyHealth; //Body Health, character die when this gage to zero 
    public float CoinFlipAdvantage; //increase to coin flip success probablilty
    public float CoinFlipCount; //increase to coin flip Repeat count
    public float Luck; //increase to gain upper grade item or increase critical hit percentage and damage
    public float MoveSpeed; //movement speed
    public int activePoint; // set order in battle phase
}

//캐릭터 기본 클래스. 이후 상속받아서 확장 가능하도록 구현.
public abstract class CharacterBase : MonoBehaviour
{
    private FCharacterStatus characterStatus;
    private EquipedItem CharacterEquiped;
    private CharacterBackpack CharacterBackpack;
    private GameObject characterObject;

    public Canvas InventoryCanvas;
    public Canvas EquipmentCanvas;

    protected BattleController battleController;
    protected event BattleController.TurnEndHandler turnEndHandle;

    public virtual void Awake()
    {
        CharacterEquiped = GetComponent<EquipedItem>();

        characterObject = this.gameObject;
    }

    public virtual void Start()
    {
        if (InventoryCanvas is null)
        {
            return;
        }

        if (EquipmentCanvas is null)
        {
            return;
        }
    }

    void OnEnable()
    {
        turnEndHandle += TurnEndAction;
    }

    void OnDisable()
    {
        turnEndHandle -= TurnEndAction;
    }

    [EasyCallFunctionNamespace.EasyCallingFunction("int", nameof(AdjustHealth))]
    public void AdjustHealth(float healthAmount)
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

    public void StartTurnAction(BattleController battleController)
    {
        this.battleController = battleController;
    }

    public void TurnEndAction()
    {
        if(battleController == null)
        {
            return;
        }

        battleController.EndTurn();
    }
}
