using Assets.Framework.Factory;
using Assets.Framework.Tools;
using Assets.Framework.UI;
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
    public int coin;
    int life;
    int nowRound;
    int DO;
    public bool isPause;
    Level level;
    //获得当前波次数据，怪物数据，路径数据
    public int currRoundkillNum;//被杀的怪物数量 判断是否进入下一回合
    //public List<RoundData> roundDataList;//当前关卡每回合的数据，主要是路径和怪物序列
    public List<int> enemyIdList;//当前回合的怪物序列
    public List<Vector3> currRoundPathList;//当前回合的路径

    EnemyBuilder enemyBuilder;
    public TowerBuilder towerBuilder;

    LevelInfoMgr lvInfoMgr;
    PlayerStatics pStatics;
    LevelInfo info;
    MapMaker mapMaker;

    public int currEnemyIDIndex;
    
    public GridPoint selectGrid;
    public TowerSetCanvas towerSetCanvas;
    public TowerSetPanel towerSetPanel;
    private void Awake()
    {
        _instance = this;
        lvInfoMgr = LevelInfoMgr.Instance;
        pStatics = PlayerStatics.Instance;
        mapMaker = GetComponent<MapMaker>();
        enemyBuilder = new EnemyBuilder();
        towerBuilder = new TowerBuilder();
        //GameObject TowerSetgo=FactoryManager.Instance.GetUI("TowerSetCanvas");
        //TowerSetgo.transform.SetParent(GameObject.Find("Canvas/Top").transform);
        //TowerSetgo.transform.localScale = Vector3.one;
        //towerSetCanvas = TowerSetgo.GetComponent<TowerSetCanvas>();

        UIManager.Instance.Show(UIPanelName.TowerSetPanel);

        currentLevel = pStatics.nowLevel;
        info = lvInfoMgr.levelInfoList[currentLevel];
        //初始化地图
        mapMaker.InitAllGrid();
        mapMaker.LoadLevelMap(currentLevel);
        //获得关卡数据和回合数据
        SetLevelData(info);

        level = new Level(info);
        level.HandleRound();

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
                if (!creatingEnemy)
                {
                    CreateEnemy();
                }
            }
        }
        else
        {
            //暂停
            StopCreateEnemy();
            creatingEnemy = false;
        }
	}

#region EnemySet
    //具体产怪方法
    private void InstantiateEnemy()
    {
        if(currEnemyIDIndex<enemyIdList.Count)
        {
            enemyBuilder.EnemyId = enemyIdList[currEnemyIDIndex];
            enemyBuilder.enemyPathList = level.roundList[level.currentRound].info.pathList;
            enemyBuilder.GetProduct();
        }
        currEnemyIDIndex++;
        if(currEnemyIDIndex>=enemyIdList.Count)
        {
            StopCreateEnemy();
        }
    }
    bool creatingEnemy;
    public void CreateEnemy()
    {
        creatingEnemy = true;
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
#endregion

    public void TowerSet()
    {
        towerBuilder.TowerId = selectGrid.gridState.towerID;
        towerBuilder.pos = selectGrid.transform.position;
        towerBuilder.GetProduct();
        
        selectGrid = null;
        UIManager.Instance.Hide(UIPanelName.TowerSetPanel);
    }
    public void RestartGame()
    {

    }
    public void SetLevelData(LevelInfo info)
    {
        coin = info.beginCoin;
        life = info.life;
        nowRound = 0;
        DO = 0;
    }
    
    public void HandleGrid(GridPoint gp)
    {
        UIManager.Instance.Hide(UIPanelName.TowerSetPanel);
        if (selectGrid==null)
        {
            selectGrid = gp;
            UIManager.Instance.Show(UIPanelName.TowerSetPanel);
            towerSetPanel.CorrectTowerSetPanel();
        }
        else if(selectGrid==gp)
        {
            selectGrid = null;
            UIManager.Instance.Hide(UIPanelName.TowerSetPanel);
            towerSetPanel.ResetPanelPos();
        }
        else
        {
            selectGrid = gp;
            UIManager.Instance.Show(UIPanelName.TowerSetPanel);
            towerSetPanel.CorrectTowerSetPanel();
        }
    }

    
}
