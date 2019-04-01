using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoundDataMgr : ScriptableObject
{
    public List<RoundData> roundDataList;
    public List<int> LevelIndexList;
    private static RoundDataMgr _instance;
    public static RoundDataMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<RoundDataMgr>("AssetData/RoundData");
                _instance.SetLevelIndex();
            }
            return _instance;
        }
    }

    void  SetLevelIndex()
    {
        LevelIndexList = new List<int>();
        LevelIndexList.Add(0);
        int id = 1;
        for(int i=0;i<roundDataList.Count;i++)
        {
            if(roundDataList[i].levelID==id+1)
            {
                LevelIndexList.Add(i);
                id++;
            }
        }
    }
}
