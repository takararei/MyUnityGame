using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
[Serializable]
public class PlayerStatics:ScriptableObject
{
    
    public int finishedLevelCount;//当前完成的关卡总数
    public int DO;//钻石数
    //成就列表记录
    public bool isMusicOff;//音乐开关
    public bool isEffectOff;//音效开关
    public List<int> levelStar;
    public List<int> itemNum;
    public List<AchievementRecord> achievementList;
    
    private static PlayerStatics _instance;
    public static PlayerStatics Instance
    {
        get
        {
            if(_instance==null)
            {
                _instance = Resources.Load<PlayerStatics>("AssetData/PlayerStatics");
            }
            return _instance;
        }
    }

    public void SetACData(AcType index,int num)
    {
        int id = (int)index;
        Instance.achievementList[id].record += num;
        if (Instance.achievementList[id].record >= AchievementInfoMgr.Instance.infoList[id].Count
            &&Instance.achievementList[id].isFinished!=true)
        {
            Instance.achievementList[id].isFinished = true;
            EventCenter.Broadcast(EventType.AcItemUpdate);
        }
    }

}
[Serializable]
public class AchievementRecord
{
    public int record;
    public bool isFinished;
}

