using UnityEngine;

//적의 위험도를 측정하는 enum, 
public enum EEnemyRiskType
{
    None,
    Low,
    Normal,
    High
}

//적 관련한 데이터를 정리해 놓은 구조체.
public struct FEnemyStatus
{
    public string name; 
    public int health;
    public int attack;
    public int defence;
    public int activepoint;
}