using Assets.Framework;
using Assets.Framework.SceneState;
using Assets.Framework.Tools;
using Assets.Framework.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel:BasePanel
{
    Button pauseBtn;
    Text Txt_TotalRound;
    Text Txt_NowRound;
    Text Txt_Life;
    Text Txt_Coin;
    LevelInfoMgr lvInfoMgr;

    //---
    GameObject enemyInfoPanel;
    Text Txt_EnemyName;
    Text Txt_NowLife;
    Text Txt_TotalLife;
    Text Txt_Heart;
    Text Txt_Def;
    Text Txt_Mdef;
    Text Txt_Speed;

    //---
    GameObject towerInfoPanel;
    Text Txt_TowerName;
    Text Txt_Phy;
    Text Txt_Magic;
    Text Txt_CD;
    Text Txt_Range;

    GridPoint selectGrid;
    BaseEnemy selectEnemy;

    public override void Init()
    {
        base.Init();
        lvInfoMgr = LevelInfoMgr.Instance;
        pauseBtn = Find<Button>("Btn_Pause");
        Txt_TotalRound = Find<Text>("Txt_TotalRound");
        Txt_NowRound = Find<Text>("Txt_NowRound");
        Txt_Life = Find<Text>("Txt_Life");
        Txt_Coin = Find<Text>("Txt_Coin");

        enemyInfoPanel = UnityTool.FindChild(rootUI, "EnemyInfo");
        towerInfoPanel = UnityTool.FindChild(rootUI, "TowerInfo");
        Txt_EnemyName = UITool.FindChild<Text>(enemyInfoPanel, "Txt_Name");
        Txt_TowerName = UITool.FindChild<Text>(towerInfoPanel, "Txt_Name");
        Txt_NowLife= UITool.FindChild<Text>(enemyInfoPanel, "Txt_NowLife");
        Txt_TotalLife= UITool.FindChild<Text>(enemyInfoPanel, "Txt_TotalLife");
        Txt_Heart= UITool.FindChild<Text>(enemyInfoPanel, "Txt_Heart");
        Txt_Def= UITool.FindChild<Text>(enemyInfoPanel, "Txt_Def");
        Txt_Mdef= UITool.FindChild<Text>(enemyInfoPanel, "Txt_Mdef");
        Txt_Speed= UITool.FindChild<Text>(enemyInfoPanel, "Txt_Speed");
        Txt_Phy = UITool.FindChild<Text>(towerInfoPanel, "Txt_Phy");
        Txt_Magic= UITool.FindChild<Text>(towerInfoPanel, "Txt_Magic");
        Txt_CD= UITool.FindChild<Text>(towerInfoPanel, "Txt_CD");
        Txt_Range= UITool.FindChild<Text>(towerInfoPanel, "Txt_Range");

        enemyInfoPanel.SetActive(false);
        towerInfoPanel.SetActive(false);
        EventCenter.AddListener<int>(EventType.Play_CoinUpdate, SetCoinNum);
        EventCenter.AddListener<int>(EventType.Play_LifeUpdate, SetLifeNum);
        EventCenter.AddListener<int>(EventType.Play_NowRoundUpdate, SetNowRound);
        EventCenter.AddListener(EventType.RestartGame, RestartGame);
        EventCenter.AddListener<GridPoint>(EventType.HandleGrid, HandleGrid);
        EventCenter.AddListener<BaseEnemy>(EventType.HandleEnemy, HandleEnemy);
        pauseBtn.onClick.AddListener(OnPauseClick);
    }

    public override void OnShow()
    {
        base.OnShow();
        LevelInfo info = lvInfoMgr.levelInfoList[GameRoot.Instance.pickLevel];
        Txt_Coin.text = info.beginCoin.ToString();
        Txt_Life.text = info.life.ToString();
        Txt_NowRound.text = "1";
        Txt_TotalRound.text = info.totalRound.ToString();
        
    }

    

    public override void OnHide()
    {
        base.OnHide();
        
    }

    public override void Update()
    {
        base.Update();
        if(towerInfoPanel.activeSelf)
        {

        }

        if(enemyInfoPanel.activeSelf)
        {
            UpdateEnemyInfoPanel();
        }
    }

    public override void Destroy()
    {
        base.Destroy();
        enemyInfoPanel.SetActive(true);
        towerInfoPanel.SetActive(true);
        selectGrid = null;
        selectEnemy = null;

        pauseBtn.onClick.RemoveAllListeners();
        EventCenter.RemoveListener<BaseEnemy>(EventType.HandleEnemy, HandleEnemy);
        EventCenter.RemoveListener<GridPoint>(EventType.HandleGrid, HandleGrid);
        EventCenter.RemoveListener(EventType.RestartGame, RestartGame);
        EventCenter.RemoveListener<int>(EventType.Play_NowRoundUpdate, SetNowRound);
        EventCenter.RemoveListener<int>(EventType.Play_LifeUpdate, SetLifeNum);
        EventCenter.RemoveListener<int>(EventType.Play_CoinUpdate, SetCoinNum);
    }

    void RestartGame()
    {
        selectEnemy = null;
        selectGrid = null;
        enemyInfoPanel.SetActive(false);
        towerInfoPanel.SetActive(false);
    }

    void SetCoinNum(int num)
    {
        Txt_Coin.text = num.ToString();
    }

    void SetNowRound(int num)
    {
        Txt_NowRound.text = num.ToString();
    }

    void SetLifeNum(int num)
    {
        Txt_Life.text = num.ToString();
    }

    private void OnPauseClick()
    {
        GameController.Instance.isPause = true;
        UIManager.Instance.Show(UIPanelName.GamePausePanel);
    }
    
    private void SetEnemyInfoPanel(EnemyInfo info)
    {
        Txt_EnemyName.text = info.Name;
        Txt_TotalLife.text = info.life.ToString();
        Txt_Def.text = info.Def.ToString();
        Txt_Magic.text = info.Mdef.ToString();
        Txt_Heart.text = info.heart.ToString();
        Txt_Speed.text = info.speed.ToString();
        //血量需要实时更新
    }

    private void SetTowerInfoPanel(TowerInfo info)
    {
        Txt_TowerName.text = info.Name;
        if(info.damageType==1)
        {
            Txt_Phy.text = info.damage.ToString();
            Txt_Magic.text = "0";
        }
        else
        {
            Txt_Phy.text = "0";
            Txt_Magic.text = info.damage.ToString();
        }

        Txt_CD.text = info.CD.ToString();
        Txt_Range.text = info.Range.ToString();
        
    }

    void UpdateEnemyInfoPanel()
    {
        if (selectEnemy == null)
        {
            return;
        }
        if(selectEnemy.gameObject.activeSelf==false)
        {
            HandleEnemy(selectEnemy);
            return;
        }
        Txt_NowLife.text = selectEnemy.currentLife.ToString();
    }

    private void HandleGrid(GridPoint gp)
    {
        if (selectGrid == null||selectGrid!=gp)
        {
            selectGrid = gp;
            //有塔就显示塔的信息
            if(selectGrid.gridState.hasTower)
            {
                towerInfoPanel.SetActive(true);
                SetTowerInfoPanel(selectGrid.baseTower.towerInfo);
                //如果显示着就实时更新
            }
            else
            {
                return;
            }
        }
        else if(selectGrid==gp)
        {
            towerInfoPanel.SetActive(false);
            selectGrid = null;
        }
        //else
        //{
        //    selectGrid = gp;
        //    enemyInfoPanel.SetActive(true);
        //    SetTowerInfoPanelActive(selectGrid.baseTower.towerInfo);
        //}
        
    }

    private void HandleEnemy(BaseEnemy enemy)
    {
        if(selectEnemy==null)
        {
            if (enemy.gameObject.activeSelf)
            {
                selectEnemy = enemy;
                enemyInfoPanel.SetActive(true);
                selectEnemy.SetEnemySign(true);
                SetEnemyInfoPanel(selectEnemy.enemyInfo);
                //更新信息
            }
            else
            {
                return;
            }
        }
        else if(selectEnemy==enemy)
        {
            selectEnemy.SetEnemySign(false);
            selectEnemy = null;
            enemyInfoPanel.SetActive(false);
        }
        else
        {
            if(enemy.gameObject.activeSelf)
            {
                selectEnemy.SetEnemySign(false);
                selectEnemy = enemy;
                selectEnemy.SetEnemySign(true);
                SetEnemyInfoPanel(selectEnemy.enemyInfo);
            }
        }
        
    }
  
}