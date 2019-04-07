using Assets.Framework.Singleton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AchievementSystem : Singleton<AchievementSystem>
{

    public PlayerData playerData;

    public override void Init()
    {
        base.Init();
        //加载玩家数据文件。
        //注册所有相关事件


        AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.First_Gold, 0);

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
        playerData.achievementList[acIndex].record +=sendCount;
        if (playerData.achievementList[acIndex].record >= needCount)
        {
            playerData.achievementList[acIndex].isFinished = true;
            //显示给玩家一个提示  成就面板显示 传入成就序号
        }
    }
}

public enum Achievement_Type
{
    First_Gold = 0,
}

