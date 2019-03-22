using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour,IBaseTower
{
    public int towerId;
    public int buildCoin;
    public int sellCoin;
    public int damageType;
    public float damageRange;
    public float CD;
    public int damage;
    public int Range;
    public int nextTowerId;
    public GameObject towerGO;
}
