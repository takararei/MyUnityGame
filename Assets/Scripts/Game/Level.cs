using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理回合，判断胜利
/// </summary>
public class Level
{
    public LevelInfo info;
    public Round[] roundList;
    public int currentRound;//从0记
    public List<RoundData> roundInfoList;
    public Level(LevelInfo lvinfo)
    {
        info = lvinfo;
        GetRoundData();
        roundList = new Round[info.totalRound];
        for(int i=0;i< info.totalRound; i++)
        {
            roundList[i] = new Round(roundInfoList[i]);
        }

        for(int i=0;i<info.totalRound;i++)
        {
            if(i== info.totalRound -1)
            {
                break;
            }
            roundList[i].SetNextRound(roundList[i + 1]);
        }
    }

    public void HandleRound()
    {
        Debug.Log(currentRound);
        if (currentRound >= info.totalRound)
        {
            //胜利
            GameController.Instance.GameWin();
            Debug.Log("胜利");
        }
        else if (currentRound == info.totalRound - 1)
        {
            //最后一波怪的UI显示音乐播放
            Debug.Log("还有最后一波");
            roundList[currentRound].Handle(currentRound);
        }
        else
        {
            roundList[currentRound].Handle(currentRound);
        }
    }

    public void AddRoundNum()
    {
        currentRound++;
    }
    void GetRoundData()
    {
        int index = RoundDataMgr.Instance.LevelIndexList[info.levelID-1];
        if (roundInfoList == null)
        {
            roundInfoList = new List<RoundData>();
        }
        else
        {
            roundInfoList.Clear();
        }
        for (int i = 0; i < info.totalRound; i++)
        {
            roundInfoList.Add(RoundDataMgr.Instance.roundDataList[index + i]);
        }
    }
}
