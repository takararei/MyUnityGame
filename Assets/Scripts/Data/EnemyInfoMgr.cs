using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyInfoMgr : ScriptableObject 
{
    public List<EnemyInfo> enemyInfoList;
    private static EnemyInfoMgr _instance;
    public static EnemyInfoMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<EnemyInfoMgr>("AssetData/EnemyInfo");
            }
            return _instance;
        }
    }
}