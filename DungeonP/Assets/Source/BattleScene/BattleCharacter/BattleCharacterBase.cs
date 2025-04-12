using UnityEngine;

//배틀씬에서 사용할 캐릭터의 기반 클래스.
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
