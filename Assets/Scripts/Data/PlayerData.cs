using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
[Serializable]
public class PlayerData
{
    public int finishedLevelCount;//当前完成的关卡总数
    public int DO;//钻石数
    //成就列表记录
    //public bool isMusicOff;//音乐开关
    //public bool isEffectOff;//音效开关
    public List<int> levelStar;
    public List<int> itemNum;
    public List<AchievementRecord> achievementList;

    public PlayerData()
    {
        DO = 0;
        finishedLevelCount = 0;
        levelStar = new List<int>();
        itemNum = new List<int>();
        achievementList = new List<AchievementRecord>();
    }



}