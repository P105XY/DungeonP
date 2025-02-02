
using System;
using UnityEngine;

[Serializable]
public class ItemSpace
{
    public int X;
    public int Y;
}

[Serializable]
public class Status
{
    public int weight;
    public int attack;
}

[Serializable]
public class CoinStatus
{
    public int luck;
}

[Serializable]
public class Item
{
    public string index;
    public string name;
    public int type;
    public ItemSpace itemspace;
    public Status status;
}

[Serializable]
public class CoinItem
{
     public string index;
    public string name;
    public ItemSpace itemspace;
    public CoinStatus coinstatus;
}

[Serializable]
public class ItemCollection
{
    public Item[] items;
}

[Serializable]
public class CoinItemCollection
{
    public CoinItem[] items;
}