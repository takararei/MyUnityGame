using Assets.Framework;
using Assets.Framework.Audio;
using Assets.Framework.Factory;
using Assets.Framework.SceneState;
using Assets.Framework.Tools;
using Assets.Framework.UI;
using Assets.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayPanel:BasePanel
{
    Button pauseBtn;
    Button startBtn;
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

    //---
    Button Btn_Bag;
    GameObject Bag_BG;
    Transform ItemContent;

    List<ItemHoldBtn> itemBtnList;

    PlayerData playerData;
    public override void Init()
    {
        base.Init();
        playerData = PlayerDataOperator.Instance.playerData;
        lvInfoMgr = LevelInfoMgr.Instance;
        pauseBtn = Find<Button>("Btn_Pause");
        startBtn = Find<Button>("Btn_Start");
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

        Btn_Bag = Find<Button>("Btn_Bag");
        Bag_BG = Find<Transform>("Bag_BG").gameObject;
        ItemContent = UITool.FindChild<Transform>(Bag_BG, "ItemContent");

        Bag_BG.SetActive(false);
        enemyInfoPanel.SetActive(false);
        towerInfoPanel.SetActive(false);
        EventCenter.AddListener<int>(EventType.Play_CoinUpdate, SetCoinNum);
        EventCenter.AddListener<int>(EventType.Play_LifeUpdate, SetLifeNum);
        EventCenter.AddListener<int>(EventType.Play_NowRoundUpdate, SetNowRound);
        EventCenter.AddListener(EventType.RestartGame, RestartGame);
        EventCenter.AddListener<GridPoint>(EventType.HandleGrid, HandleGrid);
        EventCenter.AddListener<BaseEnemy>(EventType.HandleEnemy, HandleEnemy);
        EventCenter.AddListener<Vector3>(EventType.SetStartPos, SetStartBtnPos);
        pauseBtn.onClick.AddListener(OnPauseClick);
        startBtn.onClick.AddListener(OnStartClick);
        startBtn.gameObject.SetActive(false);

        Btn_Bag.onClick.AddListener(OnBtnBGClick);

        itemBtnList = new List<ItemHoldBtn>();
        SetItemHoldBtn();
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

    public override void OnDestroy()
    {
        base.OnDestroy();
        enemyInfoPanel.SetActive(true);
        towerInfoPanel.SetActive(true);
        startBtn.gameObject.SetActive(true);
        selectGrid = null;
        selectEnemy = null;

        Bag_BG.SetActive(true);
        RemoveItemHoldBtn();
        Btn_Bag.onClick.RemoveAllListeners();
        startBtn.onClick.RemoveAllListeners();
        pauseBtn.onClick.RemoveAllListeners();
        EventCenter.RemoveListener<Vector3>(EventType.SetStartPos, SetStartBtnPos);
        EventCenter.RemoveListener<BaseEnemy>(EventType.HandleEnemy, HandleEnemy);
        EventCenter.RemoveListener<GridPoint>(EventType.HandleGrid, HandleGrid);
        EventCenter.RemoveListener(EventType.RestartGame, RestartGame);
        EventCenter.RemoveListener<int>(EventType.Play_NowRoundUpdate, SetNowRound);
        EventCenter.RemoveListener<int>(EventType.Play_LifeUpdate, SetLifeNum);
        EventCenter.RemoveListener<int>(EventType.Play_CoinUpdate, SetCoinNum);
        
    }

    void OnBtnBGClick()
    {
        Bag_BG.SetActive(!Bag_BG.activeSelf);
    }

    void RestartGame()
    {
        selectEnemy = null;
        selectGrid = null;
        enemyInfoPanel.SetActive(false);
        towerInfoPanel.SetActive(false);
        Bag_BG.SetActive(false);
        //道具的btn也要重置，取消计时
        for(int i=0;i<itemBtnList.Count;i++)
        {
            itemBtnList[i].ResetBtn();
        }
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
        AudioMgr.Instance.PlayEffectMusic(StringMgr.Button_Clip);
        GameController.Instance.isPause = true;
        UIMgr.Instance.Show(UIPanelName.GamePausePanel);
    }

    private void OnStartClick()
    {
        GameController.Instance.isPause = false;
        startBtn.gameObject.SetActive(false);
    }

    void SetStartBtnPos(Vector3 pos)
    {
        startBtn.transform.position = Camera.main.WorldToScreenPoint(pos);
        startBtn.gameObject.SetActive(true);
    }

    private void SetEnemyInfoPanel(EnemyInfo info)
    {
        Txt_EnemyName.text = info.Name;
        Txt_TotalLife.text = info.life.ToString();
        Txt_Def.text = info.Def.ToString();
        Txt_Mdef.text = info.Mdef.ToString();
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
        if(gp==null)
        {
            return;
        }
        HandleEnemy(selectEnemy);
        enemyInfoPanel.SetActive(false);
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
        if(enemy==null)
        {
            return;
        }
        EventCenter.Broadcast(EventType.HandleGrid, selectGrid);
        towerInfoPanel.SetActive(false);
        if (selectEnemy==null)
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

    void SetItemHoldBtn()
    {
        for (int i = 0; i < ItemInfoMgr.instance.itemInfoList.Count; i++)
        {
            GameObject go = FactoryMgr.Instance.GetUI(StringMgr.ItemHold);
            go.transform.SetParent(ItemContent);
            go.transform.localScale = Vector3.one;
            ItemHoldBtn item = new ItemHoldBtn(i, go);
            itemBtnList.Add(item);
        }
    }

    void RemoveItemHoldBtn()
    {
        for (int i = 0; i < itemBtnList.Count; i++)
        {
            FactoryMgr.Instance.PushUI(StringMgr.ItemHold, ItemContent.GetChild(0).gameObject);
            itemBtnList[i].Clear();
        }
        itemBtnList.Clear();
    }

    class ItemHoldBtn:BaseUIListItem
    {
        Button btn;
        Text txt_Count;
        PlayerData playerData;
        ItemInfo info;
        bool isCD;
        int tid;
        public ItemHoldBtn(int id,GameObject go)
        {
            this.id = id;
            this.root = go;
            info= ItemInfoMgr.instance.itemInfoList[id];
            playerData = PlayerDataOperator.Instance.playerData;
            btn = root.GetComponent<Button>();
            btn.onClick.AddListener(OnBtnClick);
            txt_Count = Find<Text>("Txt_ItemCount");
            UpdateState();

        }
        void UpdateState()
        {
            if (playerData.itemNum[id] > 0)
            {
                btn.interactable = true;
            }
            else
            {
                btn.interactable = false;
            }
            txt_Count.text = playerData.itemNum[id].ToString();
        }

        void OnBtnClick()
        {
            //发送事件到所有在场的敌人TODO

            //处理冷却
            playerData.itemNum[id]--;
            if (playerData.itemNum[id]==0)
            {
                //没有了 不用冷却 
            }
            else
            {
                //进行冷却操作
                float CD = info.CD;
                if (CD>0)
                {
                    //添加计时
                    isCD = true;
                    tid=GameTimer.Instance.AddTimeTask(info.CD, () =>
                    {
                        isCD = false;
                        btn.interactable = true;
                    });
                }
            }
            btn.interactable = false;
            txt_Count.text = playerData.itemNum[id].ToString();
        }

        public void ResetBtn()
        {
            //查一下数量 设置interactable
            if (isCD)
            {
                //去除计时
                GameTimer.Instance.DeleteTimeTask(tid);
                isCD = false;
            }
            UpdateState();
        }

        public override void Clear()
        {
            base.Clear();
            btn.onClick.RemoveAllListeners();
        }
    }

  
}