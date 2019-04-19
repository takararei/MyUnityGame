using Assets.Framework;
using Assets.Framework.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AchievementSystem : Singleton<AchievementSystem>
{
    public PlayerData playerData;
    PlayerDataOperator pdOperator;

    public override void Init()
    {
        base.Init();
        //加载玩家数据文件。
        //注册所有相关事件
        //pdOperator = new PlayerDataOperator();
        //playerData = pdOperator.LoadPlayerData();
        //GameRoot.Instance.data = playerData;
        //AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.First_Gold, 0);
    }

    //成就编号，要满足的条件

    void Add_Achievement_Record(Achievement_Type index, int sendCount)
    {
        //判断是否已经实现
        //获取总钻石数
        //获取条件
        int acIndex = (int)index;
        if (playerData.achievementList[acIndex].isFinished)
        {
            return;
        }

        int needCount = AchievementInfoMgr.Instance.infoList[acIndex].Count;
        playerData.achievementList[acIndex].record += sendCount;
        if (playerData.achievementList[acIndex].record >= needCount)
        {
            playerData.achievementList[acIndex].isFinished = true;
            //显示给玩家一个提示  成就面板显示 传入成就序号

            //更新成就面板，如果成就面板已经开过的话
            EventCenter.Broadcast(EventType.AcItemUpdate);
        }
    }
}

public enum Achievement_Type
{
    FirstKill = 1,
    BuildTower_30 = 2,
    Star_15 = 3,
    DO_100 = 4,
    DO_1000=5,
    DO_10000=6,
    DO_100000=7,
    Star_45=8,
    Star_all=9,
    BuildTower_100=10,
    BuildTower_1000=11,
    Kill_100=12,
    Kill_1000=13,
    Bullect_1000=14,
    Sell_10=15,
    sell_100=16,
    Buy_1=17,
    Buy_1000=18,
    Buy_10000=19,
    GetAll=20,
    
}

[Serializable]
public class AchievementRecord
{
    public int record;
    public bool isFinished;
}