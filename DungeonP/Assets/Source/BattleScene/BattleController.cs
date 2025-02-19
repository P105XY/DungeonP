using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum ETurnState
{
    NONE,
    PlayerTurn,
    EnemyTurn,
    WatingOrder
}

public class BattleController : MonoBehaviour
{
    public List<GameObject> battleCharacters = new List<GameObject>();
    public List<GameObject> battleEnemys = new List<GameObject>();
    public Dictionary <GameObject, int> ActionOrder = new Dictionary<GameObject, int>();
    
    private int turnIndex;
    private int maxturnIndex;
    private ETurnState turnState;

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

    public void SetActionOrder()
    {
         Dictionary<GameObject, int> unsortedActionOrder = new Dictionary<GameObject, int>();

        foreach(GameObject ch in battleCharacters)
        {
            if(ch is null)
            {   
                continue;
            }

            FCharacterStatus chst = ch.GetComponent<CharacterBase>().GetCharacterStat();
            
            unsortedActionOrder.Add(ch, chst.activePoint);
        }

        foreach(GameObject en in battleEnemys)
        {
            if(en is null)
            {
                continue;
            }

            FEnemyStatus enst = en.GetComponent<EnemyBase>().GetEnemyStatus();

            unsortedActionOrder.Add(en, enst.activepoint);
        }

        var sortedOrder = unsortedActionOrder.OrderBy(pair => pair.Value);
        ActionOrder = (Dictionary<GameObject, int>)sortedOrder;

        maxturnIndex = ActionOrder.Count - 1;
        turnIndex = 0;
    }

    public void TurnAction()
    {
        
    }









}

public interface ITurnAction
{
    void OnTurnStart();
    void OnTurnEnd();
}