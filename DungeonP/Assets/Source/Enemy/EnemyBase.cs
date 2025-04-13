using System.IO;
using Unity.VisualScripting;
using UnityEngine;


public abstract class EnemyBase : MonoBehaviour
{
    public EEnemyRiskType riskType { get; private set; }
    public FEnemyStatus enemyStatus { get; private set; }
    public string enemyIndex { get; private set; }
    public string[] droptable { get; private set; }

    public virtual void Start()
    {
        enemyStatus = new FEnemyStatus();

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

        FEnemyStatus ed;
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
            ed.activepoint = enemy.status.activepoint;
            droptable = enemy.droptable.enemydrops;
            enemyStatus = ed;
        }
    }
    
    public FEnemyStatus GetEnemyStatus()
    {
        return enemyStatus;
    }
}
