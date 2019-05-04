using Assets.Framework;
using Assets.Framework.Audio;
using Assets.Framework.Factory;
using Assets.Framework.Tools;
using Assets.Framework.UI;
using Assets.Framework.Util;
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
    public bool isStart;


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
    public Vector3 beginPos;//设置开始按钮

    public List<GameObject> enemyAliveList = new List<GameObject>();
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
        EventCenter.AddListener<int>(EventType.UseItemInGame, UseItem);
    }

    void Update()
    {
        
        if (!isPause&&isStart)
        {
            //产怪逻辑
            GameTimer.Instance.Update();
            if (currRoundkillNum >= enemyIdList.Count)
            {
                //添加当前回合数的索引
                if(level.currentRound>=level.info.totalRound)
                {
                    return;
                }
                AudioMgr.Instance.PlayEffectMusic(StringMgr.EnemyComing);
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

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventType.UseItemInGame, UseItem);
    }

    public void RestartGame()
    {
        //清空场上所有的敌人和子弹或者特效
        //一般都生成在GameController下
        RecycleAll();

        SetLevelData(info);
        mapMaker.LoadLevelMap(currentLevel);
        level.currentRound = 0;
        level.HandleRound();
        beginPos = level.roundInfoList[0].pathList[0];

        EventCenter.Broadcast(EventType.SetStartPos, beginPos);
        EventCenter.Broadcast(EventType.RestartGame);
        isGameOver = false;
        isPause = true;
        isStart = false;
    }

    public void RecycleAll()
    {
        int count = gameTrans.childCount;
        for (int i = 0; i < count; i++)
        {
            gameTrans.GetChild(0).SendMessage("Recycle");
        }
        timeCreatEnemy = 1;
        timeCD = 1;
        isNeedCreateEnemy = false;
        currEnemyIDIndex = 0;
        currRoundkillNum = 0;
        enemyIdList = null;
        currRoundPathList = null;
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
        towerBuilder.GetProduct();
        ChangeCoin(-selectGrid.baseTower.towerInfo.buildCoin);
        AudioMgr.Instance.PlayEffectMusic(StringMgr.BuildTower);
        EventCenter.Broadcast<GridPoint>(EventType.HandleGrid, selectGrid);
    }

    public void CreateBullect(BaseTower tower)
    {
        bullectBuilder.baseTower = tower;
        bullectBuilder.GetProduct();
    }
    //更新一下关卡数据到面板
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

    public void ChangeDO(int num)
    {
        _DO += DO;
    }

    public void ChangeRound()
    {
        EventCenter.Broadcast(EventType.Play_NowRoundUpdate, nowRound);
    }

    public void GameOver()
    {
        isGameOver = true;
        isPause = true;
        
        //保存数据 TODO 保存钻石
        PlayerDataOperator.Instance.playerData.DO += _DO;
        AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.DO_100, _DO);
        AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.DO_1000, _DO);
        AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.DO_10000, _DO);
        PlayerDataOperator.Instance.SavePlayerData();
        UIMgr.Instance.Show(UIPanelName.GameOverPanel);
    }

    public void GameWin()
    {
        isPause = true;
        
        //更新玩家记录的星级 TODO
        //如果是通关新的关卡，则更新当前的已经完成的关卡数
        int star = 1;
        if(life>=10)
        {
            star++;
        }
        if(life>=18)
        {
            star++;
        }
        PlayerData playerData = PlayerDataOperator.Instance.playerData;
        if (star>playerData.levelStar[currentLevel])
        {
            AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.Star_15, star - playerData.levelStar[currentLevel]);
            AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.Star_45, star - playerData.levelStar[currentLevel]);
            AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.Star_all, star - playerData.levelStar[currentLevel]);
            PlayerDataOperator.Instance.playerData.levelStar[currentLevel] = star;
        }
        if(currentLevel+1>playerData.finishedLevelCount)
        {
            playerData.finishedLevelCount++;
        }

        PlayerDataOperator.Instance.playerData.DO += _DO;
        AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.DO_100, _DO);
        AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.DO_1000, _DO);
        AchievementSystem.Instance.Add_Achievement_Record(Achievement_Type.DO_10000, _DO);
        PlayerDataOperator.Instance.SavePlayerData();
        UIMgr.Instance.Show(UIPanelName.GameWinPanel);
    }
    
    void UseItem(int itemType)
    {
        foreach (var item in enemyAliveList)
        {
            item.SendMessage("OnItemEffect", itemType);//TODO
        }
    }
}
