using UnityEngine;

//캐릭터의 스테이터스를 정리해 놓은 구조체.
public struct FCharacterStatus
{
    public float Health; //체력, 0이되면 사망한다.
    public float CoinFlipAdvantage; //코인 토스 시 성공확률이 나올 확률증가.
    public float CoinFlipCount; //코인을 던지는 횟수를 증가시킨다.
    public float Luck; //아이템 발견 시 고등급, 치명타 확률 증가.
    public float MoveSpeed; //이동 속도.
    public int activePoint; //전투 시 턴의 순서를 정하는 변수.
}

public enum ECharacterBorn
{
    NONE = 0,
    Nothing,
    Robber,
    Soldier,
    Smith,
    Occultist,
    Doctor
}
