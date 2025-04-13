using UnityEngine;

//배틀씬에서 사용할 캐릭터의 기반 클래스.
//배틀 씬 진입 시 기존 탐험모드 캐릭터의 스테이터스를 전달받아서 진행하도록.
public abstract class BattleCharacterBase : MonoBehaviour
{
    protected Animator currentAnimator;
    protected FCharacterStatus characterStatus;
    protected EquipedItem CharacterEquiped;
    protected BattleController battleController;
    protected event BattleController.TurnEndHandler turnEndHandle;

    void OnEnable()
    {
        turnEndHandle += TurnEndAction;
    }

    void OnDisable()
    {
        turnEndHandle -= TurnEndAction;
    }

    public void StartTurnAction(BattleController battleController)
    {
        this.battleController = battleController;
    }

    public void TurnEndAction()
    {
        if (battleController == null)
        {
            return;
        }

        battleController.EndTurn();
    }

    [EasyCallFunctionNamespace.EasyCallingFunction("int", nameof(AdjustHealth))]
    public void AdjustHealth(float healthAmount)
    {
        this.characterStatus.Health += healthAmount;
    }

    public void SetCharacterStat(FCharacterStatus characterStat)
    {
        this.characterStatus = characterStat;
    }

    public void SetCharacterEquipment(EquipedItem equipment)
    {
        this.CharacterEquiped = equipment;
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
