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
[System.Serializable]
public class TowerInfo
{
    public int towerId;
    public int buildCoin;//建造金币
    public int sellCoin;//售卖金币
    public int damageType;//伤害类型 物理or魔法 1、2
    public float damageRange;//伤害值变化范围1-1.2
    public float CD;//冷却
    public int damage;//伤害
    public int Range;//攻击范围
    public int nextTowerId;//升级的下一个塔索引
    public int bullectId;//子弹
    public string Introduce;
    public string Name;
}

[System.Serializable]
public class TowerInfoMgr:ScriptableObject
{
    public List<TowerInfo> towerInfoList;
    private static TowerInfoMgr _instance;
    public static TowerInfoMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<TowerInfoMgr>("AssetData/TowerInfo");
            }
            return _instance;
        }
    }
}
