using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
[Serializable]
public class ItemInfo
{
    public int id;
    public string name;
    public string introduce;
    public float CD;
    public float time;
    public int price;
}



public enum ItemType
{
    Life,
    Def,
    Mdef,
    Freeze,
    Slow,
}
