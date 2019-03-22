using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour,IBaseEnemy
{
    public EnemyInfo enemyInfo;
    public GameObject enemyGO;
}
[System.Serializable]
public class EnemyInfo
{
    public int enemyId;
    public int killCoin;
    public int killDO;//钻石数
    public int life;
    public float speed;
    public int Def;//物理防御
    public int Mdef;//魔法防御
    public string Name;
    public string Introduce;
}
[System.Serializable]
public class EnemyInfoMgr:ScriptableObject
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


