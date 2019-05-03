using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[System.Serializable]
public class TowerInfo
{
    public int towerId;
    public int buildCoin;//建造金币
    public int sellCoin;//售卖金币
    public int damageType;//伤害类型 物理or魔法 1、2/可能后续添加3 两者攻击都有
    public float damageRange;//伤害值变化范围1-1.2
    public float CD;//冷却
    public int damage;//伤害
    public int Range;//攻击范围
    public int nextTowerId;//升级的下一个塔索引
    public string bullectPath;//子弹
    public string Introduce;
    public string Name;
    public string path;
    public string audio;
    public string helpSprite;
}