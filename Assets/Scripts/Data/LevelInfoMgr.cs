using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
[Serializable]
public class LevelInfoMgr:ScriptableObject
{
    public List<LevelInfo> levelInfoList;
    private static LevelInfoMgr _instance;
    public static LevelInfoMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<LevelInfoMgr>("AssetData/LevelInfo");
            }
            return _instance;
        }
    }
}
