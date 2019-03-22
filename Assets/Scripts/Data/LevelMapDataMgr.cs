using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LevelMapDataMgr : ScriptableObject
{
    public List<LevelMapData> leveMapDataList;

    private static LevelMapDataMgr _instance;
    public static LevelMapDataMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<LevelMapDataMgr>("AssetData/LevelMapData");
            }
            return _instance;
        }
    }

}
