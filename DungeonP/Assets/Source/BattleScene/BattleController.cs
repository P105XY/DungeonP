using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public enum ETurnState
{
    NONE,
    PlayerTurn,
    EnemyTurn,
    WatingOrder
}

//전투 시 해당 전투의 전체를 컨트롤하는 컨트롤러 클래스.
public class BattleController : MonoBehaviour
{
    public List<GameObject> battleCharacters = new List<GameObject>();
    public List<GameObject> battleEnemys = new List<GameObject>();
    public Dictionary<GameObject, int> ActionOrder = new Dictionary<GameObject, int>();

    private int turnIndex;
    private int maxturnIndex;
    private ETurnState turnState;

    public bool bIsPlayerTurnEnd { get; private set; }
    public bool bIsEnemyTurnEnd { get; private set; }

    public delegate void TurnEndHandler();
    public event TurnEndHandler turnEndHandler;

    public void Start()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag(ObjectTagString.CharacterObjectTagString);
        GameObject[] enemys = GameObject.FindGameObjectsWithTag(ObjectTagString.EnemyTag);

        battleCharacters = characters.ToList<GameObject>();
        battleEnemys = enemys.ToList<GameObject>();
        turnIndex = 0;
        maxturnIndex = -1;

        SetActionOrder();
    }

    void OnEnable()
    {
        turnEndHandler += EndTurn;
    }

    void OnDisable()
    {
        turnEndHandler -= EndTurn;
    }

    public void SetActionOrder()
    {
        Dictionary<GameObject, int> unsortedActionOrder = new Dictionary<GameObject, int>();

        foreach (GameObject ch in battleCharacters)
        {
            if (ch is null)
            {
                continue;
            }

            FCharacterStatus chst = ch.GetComponent<CharacterBase>().GetCharacterStat();

            unsortedActionOrder.Add(ch, chst.activePoint);
        }

        foreach (GameObject en in battleEnemys)
        {
            if (en is null)
            {
                continue;
            }

            FEnemyStatus enst = en.GetComponent<EnemyBase>().GetEnemyStatus();

            unsortedActionOrder.Add(en, enst.activepoint);
        }

        var sortedOrder = unsortedActionOrder.OrderByDescending(pair => pair.Value);
        ActionOrder = sortedOrder.ToDictionary(pair => pair.Key, pair => pair.Value);

        maxturnIndex = ActionOrder.Count - 1;
        turnIndex = 0;

        TurnAction();
    }

    public void TurnAction()
    {
        if(maxturnIndex <= 0)
        {
            return;
        }

        turnIndex = turnIndex + 1;

        if (turnIndex >= maxturnIndex)
        {
            SetActionOrder();
            return;
        }

        System.Type currentTurnObjectType = ActionOrder.ElementAt(turnIndex).GetType();
        if (currentTurnObjectType == typeof(EnemyBase))
        {
            GameObject currentGameObject = ActionOrder.ElementAt(turnIndex).Key;
            EnemyBase currentEnemyBase = currentGameObject.GetComponent<EnemyBase>();
        }
        else if (currentTurnObjectType == typeof(CharacterBase))
        {
            GameObject currentGameObject = ActionOrder.ElementAt(turnIndex).Key;
            CharacterBase currentCharacterBase = currentGameObject.GetComponent<CharacterBase>();

            currentCharacterBase.StartTurnAction(this);
        }
    }

    public void EndTurn()
    {
        TurnAction();
    }
}
