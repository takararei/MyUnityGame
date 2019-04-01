using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTower : MonoBehaviour,IBaseTower
{
    public TowerInfo towerInfo;
    public GameObject towerGO;
    public GameObject bullectGO;
    public CircleCollider2D circleCollider;//范围
    private SpriteRenderer attackRangeSr;//攻击范围渲染
}



