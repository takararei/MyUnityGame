using Assets.Framework;
using Assets.Framework.Factory;
using Assets.Framework.Tools;
using Assets.Framework.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    int currentLevel;//当前关卡
    

    public bool isPause;
    public bool isGameOver;


    public int currRoundkillNum;//被杀的怪物数量 判断是否进入下一回合
    public int currEnemyIDIndex;//当前波次怪物生成到的索引

    public List<int> enemyIdList;//当前回合的怪物序列
    public List<Vector3> currRoundPathList;//当前回合的路径

    EnemyBuilder enemyBuilder;
    TowerBuilder towerBuilder;
    BullectBuilder bullectBuilder;
    LevelInfoMgr lvInfoMgr;
    MapMaker mapMaker;
    LevelInfo info;
    Level level;
    //当前关卡的一些数据记录
    public int Coin { get { return _Coin; } }
    private int _Coin;
    public int Life { get { return life; } }
    int life;
    int nowRound = 1;
    public int DO { get { return _DO; } }
    private int _DO;

    //选中的格子
    //public GridPoint selectGrid;
    //敌人、子弹、特效等生成的位置
    public Transform gameTrans;

    //用于产怪的计时器
    float timeCreatEnemy = 1;
    float timeCD = 1;
    bool isNeedCreateEnemy;
    public Vector3 beginPos;
    private void Awake()
    {
        _instance = this;
        gameTrans = UnityTool.FindChild(gameObject, "Game").transform;
        lvInfoMgr = LevelInfoMgr.Instance;
        mapMaker = GetComponent<MapMaker>();

        enemyBuilder = new EnemyBuilder();
        towerBuilder = new TowerBuilder();
        bullectBuilder = new BullectBuilder(); //TODO
        currentLevel = GameRoot.Instance.pickLevel;
        info = lvInfoMgr.levelInfoList[currentLevel];
        //初始化地图
        mapMaker.InitAllGrid();
        mapMaker.LoadLevelMap(currentLevel);
        //获得关卡数据和回合数据
        SetLevelData(info);

        level = new Level(info);
        level.HandleRound();
        beginPos=level.roundInfoList[0].pathList[0];
        EventCenter.Broadcast(EventType.SetStartPos, beginPos);
        isPause = true;
    }

    void Update()
    {
        
        if (!isPause)
        {
            //产怪逻辑
            if (currRoundkillNum >= enemyIdList.Count)
            {
                //添加当前回合数的索引
                if(level.currentRound>=level.info.totalRound)
                {
                    return;
                }
                AddRoundNum();
            }
            else
            {
                if (isNeedCreateEnemy)
                {
                    if (timeCreatEnemy >= timeCD)
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
        //清空场上所有的敌人和子弹或者特效
        //一般都生成在GameController下
        int count = gameTrans.childCount;
        for(int i=0;i<count;i++)
        {
            gameTrans.GetChild(0).SendMessage("Recycle");
        }

        SetLevelData(info);
        

        timeCreatEnemy = 1;
        timeCD = 1;
        isNeedCreateEnemy = false;
        currEnemyIDIndex = 0;
        currRoundkillNum = 0;
        enemyIdList = null;
        currRoundPathList = null;

        mapMaker.LoadLevelMap(currentLevel);
        level.currentRound = 0;
        level.HandleRound();
        beginPos = level.roundInfoList[0].pathList[0];
        EventCenter.Broadcast(EventType.SetStartPos, beginPos);
        //isGameOver = false;
        EventCenter.Broadcast(EventType.RestartGame);
        isPause = true;


    }

    
    //具体产怪方法
    private void InstantiateEnemy()
    {
        if (currEnemyIDIndex < enemyIdList.Count)
        {
            enemyBuilder.EnemyId = enemyIdList[currEnemyIDIndex];
            enemyBuilder.enemyPathList = level.roundList[level.currentRound].info.pathList;
            enemyBuilder.GetProduct();
        }
        currEnemyIDIndex++;
        if (currEnemyIDIndex >= enemyIdList.Count)
        {
            //StopCreateEnemy();
            isNeedCreateEnemy = false;
            timeCreatEnemy = 1;//下次产怪 开关一开就产怪
        }
    }

    public void CreateEnemy(List<int> list)
    {
        enemyIdList = list;
        isNeedCreateEnemy = true;
    }

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
    public void CreateTower(GridPoint selectGrid)
    {
        towerBuilder.selectGrid = selectGrid;
        //towerBuilder.TowerId = towerBuilder.selectGrid.gridState.towerID;
        //towerBuilder.pos = towerBuilder.selectGrid.transform.position;

        towerBuilder.GetProduct();
        ChangeCoin(-selectGrid.baseTower.towerInfo.buildCoin);
        EventCenter.Broadcast<GridPoint>(EventType.HandleGrid, selectGrid);

        //selectGrid = null;
        //UIManager.Instance.Hide(UIPanelName.TowerSetPanel);
    }

    public void CreateBullect(BaseTower tower)
    {
        bullectBuilder.baseTower = tower;
        bullectBuilder.GetProduct();
    }

    void SetLevelData(LevelInfo info)
    {
        _Coin = info.beginCoin;
        life = info.life;
        nowRound = 1;
        _DO = 0;
        EventCenter.Broadcast(EventType.Play_CoinUpdate, _Coin);
        EventCenter.Broadcast(EventType.Play_LifeUpdate, life);
        EventCenter.Broadcast(EventType.Play_NowRoundUpdate, nowRound);
        //钻石更新
    }
    

    public void ChangeCoin(int num)
    {
        _Coin += num;
        EventCenter.Broadcast(EventType.Play_CoinUpdate, _Coin);
    }

    public void ChangeLife(int num)
    {
        life += num;
        if (life <= 0)
        {
            life = 0;
            //游戏结束 显示结束面板
            GameOver();
        }
        EventCenter.Broadcast(EventType.Play_LifeUpdate, life);

    }

    public void ChangeRound()
    {
        EventCenter.Broadcast(EventType.Play_NowRoundUpdate, nowRound);
    }

    public void GameOver()
    {
        isGameOver = true;
        isPause = true;
        UIManager.Instance.Show(UIPanelName.GameOverPanel);
    }

    public void GameWin()
    {
        isPause = true;
        UIManager.Instance.Show(UIPanelName.GameWinPanel);
        //更新玩家记录的星级 TODO
        //如果是通关新的关卡，则更新当前的已经完成的关卡数
    }
}
