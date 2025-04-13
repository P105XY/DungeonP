using UnityEngine;

public enum EEnemyRiskType
{
    None,
    Low,
    Normal,
    High
}

public struct FEnemyStatus
{
    public string name;
    public int health;
    public int attack;
    public int defence;
    public int activepoint;
}