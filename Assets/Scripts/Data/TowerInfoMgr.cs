using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[System.Serializable]
public class TowerInfoMgr : ScriptableObject
{
    public List<TowerInfo> towerInfoList;
    private static TowerInfoMgr _instance;
    public static TowerInfoMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<TowerInfoMgr>("AssetData/TowerInfo");
            }
            return _instance;
        }
    }
}