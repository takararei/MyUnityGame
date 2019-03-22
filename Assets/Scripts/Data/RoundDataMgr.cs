using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class RoundDataMgr : ScriptableObject
{
    public List<RoundData> roundDataList;

    private static RoundDataMgr _instance;
    public static RoundDataMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<RoundDataMgr>("AssetData/RoundData");
            }
            return _instance;
        }
    }
}
