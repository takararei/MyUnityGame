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
    public int coin
    {
        get { return _Coin; }
    }

    private int _Coin;
    int life;
    int nowRound=1;
    int DO;

    public bool isPause;
    public bool isGameOver;

    Level level;
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
    public TowerSetPanel towerSetPanel;
    private void Awake()
    {
        _instance = this;
        lvInfoMgr = LevelInfoMgr.Instance;
        pStatics = PlayerStatics.Instance;
        mapMaker = GetComponent<MapMaker>();
        enemyBuilder = new EnemyBuilder();
        towerBuilder = new TowerBuilder();

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

	
	void Update ()
    {
        //if (!isPause)
        //{
        //    //产怪逻辑
        //    if (currRoundkillNum >= enemyIdList.Count)
        //    {
        //        //添加当前回合数的索引
        //        AddRoundNum();
        //    }
        //    else
        //    {
        //        if (!creatingEnemy)
        //        {
        //            CreateEnemy();//就是检查一下是不是还没有生成完毕
        //        }
        //    }
        //}
        //else
        //{
        //    //暂停
        //    StopCreateEnemy();
        //    creatingEnemy = false;
        //}

        if (!isPause)
        {
            //产怪逻辑
            if (currRoundkillNum >= enemyIdList.Count)
            {
                //添加当前回合数的索引
                AddRoundNum();
            }
            else
            {
                if(isNeedCreateEnemy)
                {
                    if(timeCreatEnemy>=timeCD)
                    {
                        InstantiateEnemy();
                        timeCreatEnemy = 0;
                    }
                    else
                    {
                        timeCreatEnemy += Time.deltaTime;
                    }
                }
            }
        }
        else
        {
            
        }
    }

    public void RestartGame()
    {

    }

    float timeCreatEnemy=1;
    float timeCD = 1;
    public bool isNeedCreateEnemy;
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
            //StopCreateEnemy();
            isNeedCreateEnemy = false;
            timeCreatEnemy = 1;
        }
    }
    //bool creatingEnemy;
    //public void CreateEnemy()
    //{
    //    creatingEnemy = true;
    //    InvokeRepeating("InstantiateEnemy", 1, 1);
    //}
    //private void StopCreateEnemy()
    //{
    //    CancelInvoke();
    //}


/// <summary>
/// 回合添加处理
/// </summary>
    public void AddRoundNum()
    {
        currEnemyIDIndex = 0;
        currRoundkillNum = 0;
        level.AddRoundNum();
        level.HandleRound();
        nowRound++;
        //更新面板上的回合显示
        ChangeRound();
    }
/// <summary>
/// 创建塔
/// </summary>
    public void CreateTower()
    {
        towerBuilder.selectGrid = selectGrid;
        towerBuilder.TowerId = towerBuilder.selectGrid.gridState.towerID;
        towerBuilder.pos = towerBuilder.selectGrid.transform.position;
        
        towerBuilder.GetProduct();
        
        selectGrid = null;
        UIManager.Instance.Hide(UIPanelName.TowerSetPanel);
    }

    
    void SetLevelData(LevelInfo info)
    {
        _Coin = info.beginCoin;
        life = info.life;
        nowRound = 1;
        DO = 0;
    }
 
    /// <summary>
    /// 处理格子，塔的创建和显示
    /// </summary>
    /// <param name="gp"></param>
    public void HandleGrid(GridPoint gp)
    {
        //UIManager.Instance.Hide(UIPanelName.TowerSetPanel);
        if (selectGrid==null)
        {
            selectGrid = gp;
            UIManager.Instance.Show(UIPanelName.TowerSetPanel);
            towerSetPanel.CorrectTowerSetPanel();
            //如果有塔就显示塔的范围
            selectGrid.TowerRange(true);
        }
        else if(selectGrid==gp)
        {
            selectGrid.TowerRange(false);
            selectGrid = null;
            UIManager.Instance.Hide(UIPanelName.TowerSetPanel);
            towerSetPanel.ResetPanelPos();
        }
        else
        {
            selectGrid.TowerRange(false);
            selectGrid = gp;
            UIManager.Instance.Show(UIPanelName.TowerSetPanel);
            towerSetPanel.CorrectTowerSetPanel();
            selectGrid.TowerRange(true);
        }
    }

    public void ChangeCoin(int num)
    {
        _Coin += num;
        EventCenter.Broadcast(EventType.Play_CoinUpdate, _Coin);
    }

    public void ChangeLife(int num)
    {
        life += num;
        if(life<=0)
        {
            life = 0;
            //游戏结束 显示结束面板
            isGameOver = true;
        }
        EventCenter.Broadcast(EventType.Play_LifeUpdate, life);

    }

    public void ChangeRound()
    {
        EventCenter.Broadcast(EventType.Play_NowRoundUpdate, nowRound);
    }

    
}
