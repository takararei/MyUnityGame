using System;
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
    public bool isPause;
    Level level;
    //获得当前波次数据，怪物数据，路径数据
    public int currRoundkillNum;
    public List<RoundData> roundDataList;
    public List<int> enemyIdList;
    public List<Vector3> currRoundPathList;

    EnemyBuilder enemyBuilder;
    TowerBuilder towerBuilder;

    LevelInfoMgr lvInfoMgr;
    PlayerStatics pStatics;
    LevelInfo info;
    MapMaker mapMaker;

    public int currEnemyIDIndex;
    private void Awake()
    {
        _instance = this;
        lvInfoMgr = LevelInfoMgr.Instance;
        pStatics = PlayerStatics.Instance;
        mapMaker = GetComponent<MapMaker>();
        enemyBuilder = new EnemyBuilder();
        towerBuilder = new TowerBuilder();

        currentLevel = pStatics.nowLevel;
        info = lvInfoMgr.levelInfoList[currentLevel];
        //初始化地图
        mapMaker.InitAllGrid();
        mapMaker.LoadLevelMap(currentLevel);
        //获得关卡数据和回合数据
        SetLevelData(info);

        level = new Level(info);
        level.HandleRound();
        //level.roundList[level.currentRound].info.pathList获取当前的波次路径

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!isPause)
        {
            //产怪逻辑
            if (currRoundkillNum >=enemyIdList.Count)
            {
                //添加当前回合数的索引
                AddRoundNum();
            }
            else
            {
                if (!creatingMonster)
                {
                    CreateEnemy();
                }
            }
        }
        else//暂停了
        {
            //暂停
            StopCreateEnemy();
            creatingMonster = false;
        }
	}

    //具体产怪方法
    private void InstantiateEnemy()
    {
        if(currEnemyIDIndex<enemyIdList.Count)
        {
            enemyBuilder.EnemyId = enemyIdList[currEnemyIDIndex];
        }
        GameObject enemyGO = enemyBuilder.GetProduct();
        enemyGO.transform.SetParent(transform);
        enemyGO.transform.position=level.roundList[level.currentRound].info.pathList[0];
        currEnemyIDIndex++;
        if(currEnemyIDIndex>=enemyIdList.Count)
        {
            StopCreateEnemy();
        }
    }

    public void SetLevelData(LevelInfo info)
    {
        coin = info.beginCoin;
        life = info.life;
        nowRound = 0;
        DO = 0;
    }

    bool creatingMonster;
    public void CreateEnemy()
    {
        creatingMonster = true;
        InvokeRepeating("InstantiateEnemy", 1, 1);
    }

    private void StopCreateEnemy()
    {
        CancelInvoke();
    }

    public void AddRoundNum()
    {
        currEnemyIDIndex = 0;
        currRoundkillNum = 0;
        level.AddRoundNum();
        level.HandleRound();
    }
}
