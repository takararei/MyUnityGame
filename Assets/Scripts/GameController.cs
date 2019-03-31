using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    int currentLevel;//当前关卡
    int coin;
    int life;
    int nowRound;
    int DO;
    //获得当前波次数据，怪物数据，路径数据



    public bool isPause;
    LevelInfoMgr lvInfoMgr;
    PlayerStatics pStatics;
    LevelInfo info;
    private void Awake()
    {
        _instance = this;
        lvInfoMgr = LevelInfoMgr.Instance;
        pStatics = PlayerStatics.Instance;
        currentLevel = pStatics.nowLevel;
        info = lvInfoMgr.levelInfoList[currentLevel];

        SetLevelData(info);

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(isPause)
        {

        }
        else
        {

        }
	}

    public void SetLevelData(LevelInfo info)
    {
        coin = info.beginCoin;
        life = info.life;
        nowRound = 0;
        DO = 0;
    }
}
