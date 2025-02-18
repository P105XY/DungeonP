using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public enum EEnemyRiskType
{
    None,
    Low,
    Normal,
    High
}

public struct FEnemyData
{
    public string name;
    public int health;
    public int attack;
    public int defence;
}

public class EnemyBase : MonoBehaviour
{
    public EEnemyRiskType riskType { get; private set; }
    public FEnemyData enemyData { get; private set; }
    public string enemyIndex { get; private set; }
    public string[] droptable { get; private set; }

    public void Start()
    {
        enemyData = new FEnemyData();

        string dbpath = null;

        switch (riskType)
        {
            case EEnemyRiskType.None:

                break;
            case EEnemyRiskType.Low:
                dbpath = ObjectValueTable.LowriskEnemyDBLocation;
                break;
            default:
                break;
        }

        if (dbpath.Length <= 0)
        {
            return;
        }

        string jsonpath = ObjectValueTable.EquipmentItemDBLocation;
        if (!File.Exists(dbpath))
        {
            Debug.Log("json file not found");
            return;
        }

        string FileData = File.ReadAllText(dbpath);

        LowriskEnemyDB enemyDB = JsonUtility.FromJson<LowriskEnemyDB>(FileData);

        FEnemyData ed;
        foreach (LowriskEnemy enemy in enemyDB.lowriskenemys)
        {
            if (!enemy.index.Equals(enemyIndex))
            {
                continue;
            }

            ed.name = enemy.name;
            ed.health = enemy.status.health;
            ed.attack = enemy.status.attack;
            ed.defence = enemy.status.defence;
            droptable = enemy.droptable.enemydrops;
            enemyData = ed;
        }
    }

    private void Dead()
    {
        foreach (string itemIndex in droptable)
        {
            
        }
    }
}
