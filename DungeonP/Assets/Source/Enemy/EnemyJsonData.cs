using System;
using UnityEngine;


[Serializable]
public class EnemyStatus
{
    public int health;
    public int attack;
    public int defence;
}

[Serializable]
public class EnemyDropTable
{
    public string[] enemydrops;
}

[Serializable]
public class LowriskEnemy
{
    public string index;
    public string name;
    public EnemyStatus status;
    public EnemyDropTable droptable;
}

[Serializable]
public class LowriskEnemyDB
{
    public LowriskEnemy[] lowriskenemys;
}
